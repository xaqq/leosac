using System.Net;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace LeosacIntegration;

public class LeosacDockerHelper : IAsyncDisposable
{
    public string DockerNetworkName;
    public string LeosacDockerServiceName;
    public string PostgresDockerServiceName;
    public const string LeosacImageName = "leosac_test";
    public const string PostgresImageName = "postgres:9.6";

    private string LeosacServiceId;
    private string LeosacContainerId;
    private string PostgresServiceId;
    private string PostgresContainerId;

    private string NetworkId;

    private string TestName;
    private string TestDataDirectory;
    DockerClient client;

    private async Task<uint?> GetPublishedPortForService(uint port, string serviceId)
    {
        var service = await client.Swarm.InspectServiceAsync(serviceId);
        foreach (var configuredPort in service.Endpoint.Ports)
        {
            if (configuredPort.TargetPort == port)
            {
                return configuredPort.PublishedPort;
            }
        }

        return null;
    }

    public async Task<uint> GetPostgresPublishedPort()
    {
        var port = await GetPublishedPortForService(5432, PostgresServiceId);
        if (port == null)
        {
            throw new ApplicationException("Cannot find pgsql published port");
        }

        return (uint) port;
    }

    public async Task<uint> GetLeosacWebsocketPublishedPort()
    {
        var port = await GetPublishedPortForService(8888, LeosacServiceId);
        if (port == null)
        {
            throw new ApplicationException("Cannot find leosac published port");
        }

        return (uint) port;
    }

    public LeosacDockerHelper(string testDataDirectory, string testName)
    {
        TestName = testName;
        TestDataDirectory = testDataDirectory;
        client = new DockerClientConfiguration(
                new Uri("unix:///var/run/docker.sock"))
            .CreateClient();

        DockerNetworkName = $"lt_{testName}";
        LeosacDockerServiceName = $"lt_{testName}_d";
        PostgresDockerServiceName = $"lt_{testName}_pg";
    }

    private async Task CleanService(string serviceName)
    {
        var services = await client.Swarm.ListServicesAsync(new ServicesListParameters
        {
            Filters = new ServiceFilter
            {
                Name = new[] {serviceName},
            }
        });

        // Scale down services.
        foreach (var service in services)
        {
            Console.WriteLine($"Scaling down service {serviceName} with id {service.ID} to 0 replicas...");
            var p = new ServiceUpdateParameters();
            p.Version = (long) service.Version.Index;
            p.Service = service.Spec;
            p.Service.Mode = new ServiceMode
            {
                Replicated = new ReplicatedService()
                {
                    Replicas = 0
                }
            };
            await client.Swarm.UpdateServiceAsync(service.ID, p);

            while (true)
            {
                Console.WriteLine("Waiting for service to scale down...");
                var x2 = await client.Swarm.ListServicesAsync(new ServicesListParameters
                {
                    Filters = new ServiceFilter
                    {
                        Name = new[] {serviceName},
                    }
                });
                if (!x2.Any())
                {
                    // Prob exited, good.
                    break;
                }

                if (x2.First().ServiceStatus == null)
                {
                    break;
                }
                if (x2.First().ServiceStatus?.RunningTasks == 0)
                {
                    break;
                }

                await Task.Delay(1000);
            }
        }

        foreach (var service in services)
        {
            await client.Swarm.RemoveServiceAsync(service.ID);
            while (true)
            {
                var x2 = await client.Swarm.ListServicesAsync(new ServicesListParameters
                {
                    Filters = new ServiceFilter
                    {
                        Name = new[] {serviceName},
                    }
                });
                if (!x2.Any())
                {
                    break;
                }

                await Task.Delay(1000);
            }
        }

        // Wait for associated tasks to finish
        foreach (var service in services)
        {
            while (true)
            {
                var filters = new Dictionary<string, IDictionary<string, bool>>();
                filters["name"] = new Dictionary<string, bool>();
                filters["name"][serviceName] = true;

                try
                {
                    var x2 = await client.Tasks.ListAsync(new TasksListParameters
                    {
                        Filters = filters
                    });
                    if (!x2.Any())
                    {
                        Console.WriteLine(
                            $"Waiting for tasks of service {serviceName} (with id {service.ID}) to finish");
                        break;
                    }
                }
                catch (DockerApiException e)
                {
                    if (e.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Good news, already deleted
                        break;
                    }
                }

                await Task.Delay(1000);
            }
        }
    }

    private async Task CreateLeosacService()
    {
        var testDataSource = Environment.GetEnvironmentVariable("LEOSAC_INTEGRATION_TEST_DATA_ROOT") ??
                             "/home/user/leosac/tests/";
        var coverageResultRoot = Environment.GetEnvironmentVariable("LEOSAC_INTEGRATION_COVERAGE_RESULT_ROOT") ??
                                 $"{Directory.GetCurrentDirectory()}/coverage_result";
        var coverageResultDirectory = $"{coverageResultRoot}/{TestName}";
        Directory.CreateDirectory(coverageResultDirectory);
        ServiceCreateParameters p = new ServiceCreateParameters
        {
            Service = new ServiceSpec
            {
                Name = LeosacDockerServiceName,
                TaskTemplate = new TaskSpec
                {
                    ContainerSpec = new ContainerSpec
                    {
                        // 1 minute
                        StopGracePeriod = (long)1000000000 * 60,
                        Image = LeosacImageName,
                        Mounts = new List<Mount>
                        {
                            new Mount
                            {
                                Target = "/test_data/",
                                Source = $"/{testDataSource}/{TestDataDirectory}/"
                            },
                            new Mount
                            {
                                Target = "/coverage_result/",
                                Source = coverageResultDirectory
                            }
                        },
                        Command = new[]
                        {
                            "/bin/bash",
                            "/entrypoint.sh",
                            "-k",
                            "/test_data/kernel.xml"
                        },
                        TTY = true,
                    },
                    Networks = new List<NetworkAttachmentConfig>
                    {
                        new()
                        {
                            Target = NetworkId,
                            Aliases = new List<string>
                            {
                                "leosac"
                            }
                        }
                    },
                    RestartPolicy = new SwarmRestartPolicy
                    {
                        Condition = "none",
                    },
                },
                // Expose Leosac websocket API.
                EndpointSpec = new EndpointSpec
                {
                    Ports = new List<PortConfig>
                    {
                        new PortConfig
                        {
                            Protocol = "tcp",
                            TargetPort = 8888,
                        }
                    }
                }
            }
        };

        var response = await client.Swarm.CreateServiceAsync(p);
        LeosacServiceId = response.ID;
    }

    private async Task CreatePostgresService()
    {
        ServiceCreateParameters p = new ServiceCreateParameters
        {
            Service = new ServiceSpec
            {
                Name = PostgresDockerServiceName,
                TaskTemplate = new TaskSpec
                {
                    ContainerSpec = new()
                    {
                        Image = PostgresImageName,
                        Env = new List<string>
                        {
                            "POSTGRES_PASSWORD=leosac123",
                            "POSTGRES_DB=leosac"
                        },
                    },
                    Networks = new List<NetworkAttachmentConfig>
                    {
                        new()
                        {
                            Target = NetworkId,
                            Aliases = new List<string>
                            {
                                "postgres"
                            }
                        }
                    },
                    RestartPolicy = new SwarmRestartPolicy
                    {
                        Condition = "none",
                    },
                },

                // Expose PGSQL.
                EndpointSpec = new EndpointSpec
                {
                    Ports = new List<PortConfig>
                    {
                        new PortConfig
                        {
                            Protocol = "tcp",
                            TargetPort = 5432,
                        }
                    }
                }
            }
        };

        var response = await client.Swarm.CreateServiceAsync(p);
        PostgresServiceId = response.ID;
    }

    public async Task Start()
    {
        await CleanupDocker();

        await CreateNetwork();
        await CreatePostgresService();
        PostgresContainerId = await GetContainerIdForServiceId(PostgresServiceId, PostgresDockerServiceName);

        await CreateLeosacService();
        LeosacContainerId = await GetContainerIdForServiceId(LeosacServiceId, LeosacDockerServiceName);
        await WaitForContainerToStart(PostgresContainerId);
        await WaitForContainerToStart(LeosacContainerId);


        /*var logProgess = new Progress<string>(msg => { Console.WriteLine(msg); });

        ContainerLogsParameters logsParameters = new ContainerLogsParameters
        {
            Follow = true,
            ShowStderr = true,
            ShowStdout = true,
        };

        CancellationTokenSource cts = new CancellationTokenSource();
        cts.CancelAfter(10000);
        Console.WriteLine("Start stream container log");
        await client.Containers.GetContainerLogsAsync(LeosacContainerId,
            logsParameters, cts.Token,
            logProgess);*/
    }

    private async Task CleanupDocker()
    {
        await CleanService(PostgresDockerServiceName);
        await CleanService(LeosacDockerServiceName);
        await CleanNetwork(DockerNetworkName);
    }

    private async Task WaitForContainerToStart(string containerId)
    {
        for (int i = 0; i < 10; ++i)
        {
            Console.WriteLine("Waiting for container to start ...");
            var containerInfo = await client.Containers.InspectContainerAsync(containerId);
            if (containerInfo.State.Running)
                return;
            await Task.Delay(2000);
        }
    }

    private async Task CleanNetwork(string networkName)
    {
        var filters = new Dictionary<string, IDictionary<string, bool>>();
        filters["name"] = new Dictionary<string, bool>();
        filters["name"][networkName] = true;

        var networks = await client.Networks.ListNetworksAsync(new NetworksListParameters
        {
            Filters = filters
        });
        foreach (var network in networks)
        {
            // Listing of network is doing matching, not equality. So make sure we got the correct network
            if (network.Name == networkName)
            {
                Console.WriteLine($"Removing network with id {network.ID}");
                await client.Networks.DeleteNetworkAsync(network.ID);
            }
        }
    }

    private async Task CreateNetwork()
    {
        var network = await client.Networks.CreateNetworkAsync(new NetworksCreateParameters
        {
            Name = DockerNetworkName,
            Scope = "swarm",
            Driver = "overlay"
        });
        NetworkId = network.ID;
    }

    private async Task<string> GetContainerIdForServiceId(string serviceId, string serviceName)
    {
        for (int i = 0; i < 10; ++i)
        {
            var filters = new Dictionary<string, IDictionary<string, bool>>();
            filters["service"] = new Dictionary<string, bool>();
            filters["service"][serviceName] = true;
            var tasks = await client.Tasks.ListAsync(new TasksListParameters
            {
                Filters = filters
            });
            var taskResponse = tasks.FirstOrDefault();
            if (taskResponse == null)
            {
                await Task.Delay(2000);
                continue;
            }

            if (taskResponse.ServiceID != serviceId)
            {
                throw new ApplicationException("Docker service mismatch");
            }

            if (taskResponse.Status.State == TaskState.Failed ||
                taskResponse.Status.State == TaskState.Rejected)
            {
                throw new ApplicationException($"Docker tasks issue: status = {taskResponse.Status.State}");
            }

            if (taskResponse.Status.ContainerStatus != null)
            {
                return taskResponse.Status.ContainerStatus.ContainerID;
            }

            await Task.Delay(2000);
        }

        throw new ApplicationException("Could not find container for service");
    }

    public async Task DropPostgres(CancellationToken cancellationToken)
    {
        await client.Swarm.RemoveServiceAsync(PostgresServiceId, cancellationToken);
        // Wait for service drop.
        while (true)
        {
            var filter = new ServiceFilter
            {
                Id = new[] {PostgresServiceId}
            };
            var inspection = await client.Swarm.ListServicesAsync(new ServicesListParameters
            {
                Filters = filter
            }, cancellationToken);
            if (inspection != null && !inspection.Any())
                break;
            await Task.Delay(1000, cancellationToken);
        }

        // Now make sure the previous container id for postgres is down too
        while (true)
        {
            try
            {
                var containerInfo =
                    await client.Containers.InspectContainerAsync(PostgresContainerId, cancellationToken);
                if (containerInfo.State.Dead)
                    return;
            }
            catch (DockerContainerNotFoundException)
            {
                // Good, means container is not here anymore.
                return;
            }

            await Task.Delay(2000, cancellationToken);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await CleanupDocker();
    }
}
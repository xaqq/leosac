using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using LeosacAPI;
using LeosacDB;
using LeosacIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace LeosacIntegrationTests;

public class LeosacTestBase : IAsyncLifetime
{
    protected ServiceProvider _serviceProvider;
    protected LeosacDockerHelper helper;
    protected LeosacApi _api;
    protected CancellationToken _cancellationToken;

    private string LeosacUrl;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="testDataDirectory">What data directory (from git_root/tests/xxx) is mounted in docker</param>
    /// <param name="testName">An identifier for the test. Is used to created named docker service.</param>
    public LeosacTestBase(string testDataDirectory, string testName)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging(c => c.AddConsole());
        _serviceProvider = serviceCollection.BuildServiceProvider();

        var cts = new CancellationTokenSource();
        cts.CancelAfter(120 * 1000);
        _cancellationToken = cts.Token;
        helper = new LeosacDockerHelper(testDataDirectory, testName);
    }

    public async Task InitializeAsync()
    {
        await helper.Start();
        var leosacPort = await helper.GetLeosacWebsocketPublishedPort();
        LeosacUrl = $"ws://127.0.0.1:{leosacPort}";

        _api = await CreateAndWaitLeosacApi();
    }

    public Task DisposeAsync()
    {
        helper.DisposeAsync();
        return Task.CompletedTask;
    }

    protected async Task<leosacContext> GetDbContext()
    {
        var y = new DbContextOptionsBuilder<leosacContext>();

        var port = await helper.GetPostgresPublishedPort();

        y.UseNpgsql($"Host=127.0.0.1;Port={port};Database=leosac;Username=postgres;Password=leosac123");
        return new leosacContext(y.Options);
    }

    protected async Task<LeosacApi> CreateAndWaitLeosacApi()
    {
        while (true)
        {
            _cancellationToken.ThrowIfCancellationRequested();
            try
            {
                var ws = new ClientWebSocket();
                await ws.ConnectAsync(new Uri(LeosacUrl), _cancellationToken);

                return new LeosacApi(ws, _serviceProvider.GetService<ILogger<LeosacApi>>()!);
            }
            catch (WebSocketException ex)
            {
                if (ex.WebSocketErrorCode == WebSocketError.Faulted)
                {
                    // Most likely websocket server is not here yet.
                    await Task.Delay(1000, _cancellationToken);
                }
            }
        }
    }
}
// See https://aka.ms/new-console-template for more information

using LeosacDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LeosacIntegration;

class Program
{

    class MyService : BackgroundService
    {
        private ILogger<MyService> _logger;
        private IServiceProvider _provider;
        
        public MyService(ILogger<MyService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dockerHelper = new LeosacDockerHelper("test_start_with_postgres", "lol");
            try
            {
                await dockerHelper.Start();
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Stop watching containers");
            }

            var y = new DbContextOptionsBuilder<leosacContext>();

            var port = await dockerHelper.GetPostgresPublishedPort();
            
            y.UseNpgsql($"Host=127.0.0.1;Port={port};Database=leosac;Username=postgres;Password=leosac123");
            using (var db = new leosacContext(y.Options))
            {
                var schemaVersion = await db.SchemaVersions.FirstAsync(x => x.Name == "core");
                if (schemaVersion.Version != 1)
                {
                    _logger.LogError("FAILED VERSION");
                }
            }
        }
    }
    
    static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        return host.RunAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<MyService>();
                services.AddDbContext<leosacContext>(options => options.UseNpgsql());
            })
            .ConfigureLogging((_, logging) => 
            {
                logging.ClearProviders();
                logging.AddSimpleConsole(options => options.IncludeScopes = true);
            });
}
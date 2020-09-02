using System;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UserManagerService.DAL.Core;

namespace UserManagerService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                .UseSetting(WebHostDefaults.SuppressStatusMessagesKey, "True")
                .Build();
            
            var provider = host.Services;

            using (var scope = provider.CreateScope())
            {
                await InitializeDataBase(scope.ServiceProvider);
            }

            await host.RunAsync();
        }

        private static async Task InitializeDataBase(IServiceProvider services)
        {
            UserManagementSeed.CreateDataBaseIfNotExists(services);
            var runner = services.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
            var dbSeeder = services.GetRequiredService<DbInitializer>();
            await dbSeeder.Initialize();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseShutdownTimeout(TimeSpan.FromSeconds(10))
                .UseStartup<Startup>();
        }
    }
}
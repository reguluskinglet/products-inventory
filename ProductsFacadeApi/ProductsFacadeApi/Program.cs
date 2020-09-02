using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductsFacadeApi.DAL.Core;

namespace ProductsFacadeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            var provider = host.Services;
            var logger = provider.GetService<ILogger>();
            
            // logger.LogInformation("Начало запуска сервиса OperatorFacadeApi.");

            try
            {
                using (var scope = provider.CreateScope())
                {
                    InitializeDataBase(scope.ServiceProvider);
                }

                host.Run();
            }
            catch (Exception e)
            {
                // logger.LogError("Ошибка при старте сервиса.", e);
            }

            // logger.LogInformation("Завершение сервиса OperatorFacadeApi.");
        }

        /// <summary>
        /// Метод для запуска раннера миграций.
        /// </summary>
        /// <param name="services"></param>
        private static void InitializeDataBase(IServiceProvider services)
        {
            // var logger = services.GetRequiredService<ILogger>();

            // logger.LogInformation("Актуализация базы данных.");

            DatabaseSeed.CreateDataBaseIfNotExists(services);

            var runner = services.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();

            // logger.LogInformation("Актуализация базы данных успешно завершена.");
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseShutdownTimeout(TimeSpan.FromSeconds(10))
                .UseStartup<Startup>();
        }
    }
}
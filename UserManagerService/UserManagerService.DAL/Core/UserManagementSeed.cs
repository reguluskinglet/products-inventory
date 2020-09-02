﻿using System;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace UserManagerService.DAL.Core
{
    /// <summary>
    /// Создание и инициализация БД
    /// </summary>
    public static class UserManagementSeed
    {
        /// <summary>
        /// Создание БД если нет
        /// </summary>
        public static void CreateDataBaseIfNotExists(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var connection = configuration.GetSection("ConnectionStrings")["DatabaseConnection"];

            string[] parameters = connection.Split(";");
            var paramName = "Database";
            var databaseParam = parameters.First(s => s.StartsWith(paramName, StringComparison.InvariantCultureIgnoreCase))?.Split('=');

            if (databaseParam?.Length != 2)
            {
                throw new InvalidOperationException($"Parameter \"{paramName}\" not found in connection string.");
            }

            var catalog = databaseParam[1];
            connection = string.Join(";", parameters.Where(s => !s.StartsWith(paramName, StringComparison.InvariantCultureIgnoreCase)));

            var databasesQuery = "select * from postgres.pg_catalog.pg_database where datname = @name";
            var createDatabaseQuery = $"CREATE DATABASE \"{catalog}\"";

            using var db = new NpgsqlConnection(connection);
            if (!db.Query(databasesQuery, new { name = catalog }).Any())
            {
                db.Execute(string.Format(createDatabaseQuery, catalog));
            }
        }
    }
}

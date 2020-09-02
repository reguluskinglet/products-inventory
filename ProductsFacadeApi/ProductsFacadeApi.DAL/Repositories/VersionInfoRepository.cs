using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ProductsFacadeApi.DAL.Abstractions;

namespace ProductsFacadeApi.DAL.Repositories
{
    /// <inheritdoc/>
    public class VersionInfoRepository : IVersionInfoRepository
    {
        private readonly string _connection;

        /// <inheritdoc />
        public VersionInfoRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DatabaseConnection");
        }

        /// <inheritdoc/>
        public async Task<int> Count()
        {
            using (var db = OpenConnection())
            {
                return await db.QueryFirstAsync<int>("SELECT Count(0) FROM \"VersionInfo\"");
            }
        }

        /// <summary>
        /// Получить подключение.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private IDbConnection OpenConnection(string connection = null)
        {
            var npgsql = new NpgsqlConnection(connection ?? _connection);
            npgsql.Open();
            return npgsql;
        }
    }
}

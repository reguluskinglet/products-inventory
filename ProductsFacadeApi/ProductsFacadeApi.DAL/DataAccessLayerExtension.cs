using Microsoft.Extensions.DependencyInjection;
using ProductsFacadeApi.DAL.Abstractions;
using ProductsFacadeApi.DAL.Repositories;

namespace ProductsFacadeApi.DAL
{
    /// <summary>
    /// Расширение для добавления прикладных репозиториев.
    /// </summary>
    public static class DataAccessLayerExtension
    {
        /// <summary>
        /// Добавление списка прикладных репозиториев в коллекцию.
        /// </summary>
        public static void AddDataAccessLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IVersionInfoRepository, VersionInfoRepository>();
            serviceCollection.AddTransient<IProductsRepository, ProductsRepository>();
        }
    }
}

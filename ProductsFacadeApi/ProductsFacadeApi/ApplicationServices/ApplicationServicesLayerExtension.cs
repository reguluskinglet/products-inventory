using Microsoft.Extensions.DependencyInjection;

namespace ProductsFacadeApi.ApplicationServices
{
    /// <summary>
    /// Расширение для работы с прикладными сервисами.
    /// </summary>
    public static class ApplicationServicesLayerExtension
    {
        /// <summary>
        /// Добавление прикладных сервисов в коллекцию.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddApplicationServicesLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ProductsService>();
            serviceCollection.AddTransient<UsersService>();
        }
    }
}
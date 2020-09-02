﻿using Microsoft.Extensions.DependencyInjection;

namespace UserManagerService.ApplicationServices
{
    /// <summary>
    /// Класс для расширение возможностей стандартной коллекции сервисов
    /// </summary>
    public static class ApplicationLayerExtension
    {
        /// <summary>
        /// Добавление прикладных сервисов к коллекции сервисов
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddApplicationLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<UsersService>();
        }
    }
}

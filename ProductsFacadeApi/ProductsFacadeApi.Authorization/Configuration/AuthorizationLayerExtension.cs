using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProductsFacadeApi.Authorization.Filters;
using ProductsFacadeApi.Authorization.Middlewares;

namespace Authorization.Configuration
{
    /// <summary>
    /// Расширение для работы с авторизацией.
    /// </summary>
    public static class AuthorizationLayerExtension
    {
        /// <summary>
        /// Добавление механизма авторизации.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void UseAuthorizationMiddleware(this IApplicationBuilder serviceCollection)
        {
            serviceCollection.UseMiddleware<AuthorizationMiddleware>();
        }
        
        /// <summary>
        /// Добавление поддержки авторизации.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddUserAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<UserAuthorizationFilter>();
        }
    }
}
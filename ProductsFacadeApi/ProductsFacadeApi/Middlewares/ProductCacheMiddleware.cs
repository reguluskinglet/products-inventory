using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductsFacadeApi.ApplicationServices;
using ProductsFacadeApi.Authorization.Contexts;
using ProductsFacadeApi.DAL.Abstractions;
using ProductsFacadeApi.DDD;
using ProductsFacadeApi.Domain.Entities;
using ProductsFacadeApi.Infrastructure.Dto;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace ProductsFacadeApi.Middlewares
{
    /// <summary>
    /// Middleware для кэширования товаров для клиента.
    /// </summary>
    public class ProductCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRedisCacheClient _cache;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="cache"></param>
        public ProductCacheMiddleware(RequestDelegate next,
            IRedisCacheClient cache)
        {
            _next = next;
            _cache = cache;
        }

        /// <summary>
        /// Вызов метода Invoke для ProductCacheMiddleware.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var query = httpContext.Request.Query;

            if (query.ContainsKey("pageIndex") && query.ContainsKey("pageSize"))
            {
                await GetCachedProductsPage(httpContext, query);
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }

        private async Task GetCachedProductsPage(HttpContext httpContext, IQueryCollection query)
        {
            var pageIndex = 0;
            var pageSize = 10;

            var userId = AuthorizationContext.CurrentUserId.ToString();

            if (!int.TryParse(query["pageIndex"], out pageIndex) && !int.TryParse(query["pageSize"], out pageSize))
            {
                await _next.Invoke(httpContext);
                return;
            }

            var userProductsPageCache = await _cache.GetDbFromConfiguration().GetAsync<ProductsPageDto>("user:key");
            if (userProductsPageCache == null)
            {
                await _next.Invoke(httpContext);
                return;
            }

            var cachedItems = userProductsPageCache.Products
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (cachedItems.Count == 0)
            {
                await _next.Invoke(httpContext);
                return;
            }

            var resultCachedData = new ProductsPageDto()
            {
                Products = cachedItems,
                TotalCount = userProductsPageCache.TotalCount,
                IsCached = userProductsPageCache.IsCached,
            };

            var serializedCache = JsonConvert.SerializeObject(resultCachedData, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await httpContext.Response.WriteAsync(serializedCache);
        }
    }
}

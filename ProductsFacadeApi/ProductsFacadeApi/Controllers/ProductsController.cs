using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductsFacadeApi.ApplicationServices;
using ProductsFacadeApi.Authorization.Contexts;
using ProductsFacadeApi.Domain.Entities;
using ProductsFacadeApi.Infrastructure.Dto;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace ProductsFacadeApi.Controllers
{
    /// <summary>
    /// Контроллер товаров.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRedisCacheClient _cache;
        private readonly ProductsService _productsService;
        
        public ProductsController(
            ProductsService productsService,
            IRedisCacheClient cache
        )
        {
            //_logger = logger;
            _cache = cache;
            _productsService = productsService;
        }

        /// <summary>
        /// Получить список товаров.
        /// </summary>
        [HttpGet("page")]
        public async Task<ActionResult<ProductsPageDto>> GetProducts(PageDto pageDto)
        {
            var productsListResult = await _productsService.GetProductsList(pageDto);
            if (productsListResult.IsFailure)
            {
                //_logger.LogError($"{productsListResult}");
                return BadRequest(productsListResult.Value);
            }

            var cacheResult = await SetProductsPageToCache(productsListResult.Value);
            if (cacheResult.IsFailure)
            {
                return BadRequest(cacheResult.Error);
            }

            return Ok(productsListResult.Value);
        }

        /// <summary>
        /// Добавить новый товар.
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult> AddProduct([FromBody] AddProductDto productDto)
        {
            if (productDto.Title == null)
            {
                return BadRequest("Ниименование товара пустое.");
            }

            if (productDto.Price == 0)
            {
                return BadRequest("Не указана цена товара.");
            }

            var addProductResult = await _productsService.AddProductAsync(productDto);
            if (addProductResult.IsFailure)
            {
                return BadRequest("Не удалось создать новый товар.");
            }

            var cacheResult = await RemoveProductsCache();
            if (cacheResult.IsFailure)
            {
                return BadRequest(cacheResult.Error);
            }

            return Ok();
        }

        private async Task<Result> SetProductsPageToCache(ProductsPageDto productsPageDto)
        {
            var currentUserProductPageCache = await _cache.GetDbFromConfiguration().GetAsync<ProductsPageDto>("user:key");

            var currentUserProducts = productsPageDto.Products;
            if (currentUserProductPageCache != null)
            {
                currentUserProducts = currentUserProducts
                    .Concat(currentUserProductPageCache.Products)
                    .ToList();
            }

            var resultCachedData = new ProductsPageDto()
            {
                Products = currentUserProducts,
                TotalCount = productsPageDto.TotalCount,
                IsCached = true,
            };

            var redisResult = await _cache.GetDbFromConfiguration().AddAsync("user:key", resultCachedData);
            if (!redisResult)
            {
                return Result.Failure("Не удалось обновить список продуктов в Redis cache.");
            }

            return Result.Success();
        }

        private async Task<Result> RemoveProductsCache()
        {
            var currentUserCacheExists = await _cache.GetDbFromConfiguration().ExistsAsync("user:key");
            if (currentUserCacheExists)
            {
                var redisResult = await _cache.GetDbFromConfiguration().RemoveAsync("user:key");
                if (!redisResult)
                {
                    return Result.Failure("Could not delete products from Redis cache.");
                }
            }

            return Result.Success();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductsFacadeApi.ApplicationServices;
using ProductsFacadeApi.Authorization.Contexts;
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

            var redisResult = await _cache.GetDbFromConfiguration().AddAsync("user:key", 
                productsListResult.Value.Products);
            if (!redisResult)
            {
                return BadRequest("Could not add products to Redis cache.");
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

            var redisResult = await _cache.GetDbFromConfiguration().RemoveAsync("user:key");
            if (!redisResult)
            {
                return BadRequest("Could not delete products from Redis cache.");
            }
            
            return Ok();
        }
    }
}
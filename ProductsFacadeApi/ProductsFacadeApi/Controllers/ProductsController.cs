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
        private readonly IDistributedCache _cache;
        private readonly ProductsService _productsService;
        
        public ProductsController(
            ProductsService productsService,
            IDistributedCache cache
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

            await _cache.SetStringAsync(AuthorizationContext.CurrentUserId.ToString(), JsonConvert.SerializeObject(
                new ProductCacheDto()
                {
                    Products = productsListResult.Value.Products,
                    TotalCount = productsListResult.Value.TotalCount,
                    PageIndex = pageDto.PageIndex,
                }));

            return Ok(productsListResult.Value);
        }
    }
}
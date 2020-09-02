using AutoMapper;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using ProductsFacadeApi.DAL.Abstractions;
using ProductsFacadeApi.Infrastructure.Dto;
using ProductsFacadeApi.DDD;
using ProductsFacadeApi.Domain.Entities;

namespace ProductsFacadeApi.ApplicationServices
{
    /// <summary>
    /// Сервис товаров.
    /// </summary>
    public class ProductsService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly IProductsRepository _productsRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="productsRepository"></param>
        public ProductsService(
            IMapper mapper,
            UnitOfWork unitOfWork,
            
            IProductsRepository productsRepository)
        {
            //_logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
        }

        /// <summary>
        /// Получить список товаров.
        /// </summary>
        public async Task<Result<ProductsPageDto>> GetProductsList(PageDto pageDto)
        {
            using (_unitOfWork.Begin())
            {
                var productsPage = await _productsRepository.GetProductsPageAsync(pageDto.PageSize, pageDto.PageIndex);
                if (productsPage == null)
                {
                    var message = $"Список продуктов пуст для страницы";
                    //_logger.LogWarning(message);
                    return Result.Failure<ProductsPageDto>(message);
                }

                var mappedProductsPageResult = _mapper.Map<ProductsPage, ProductsPageDto>(productsPage);
                return Result.Success(mappedProductsPageResult);
            }
        }
    }
}
using System.Collections.Generic;
using ProductsFacadeApi.Domain.Entities;
using ProductsFacadeApi.Infrastructure.Dto;

namespace ProductsFacadeApi.Infrastructure.Dto
{
    /// <summary>
    /// Dto для страницы товаров.
    /// </summary>
    public class ProductsPageDto
    {
        /// <summary>
        /// Список контактов
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Общее количество продуктов.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
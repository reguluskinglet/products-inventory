using System.Collections.Generic;
using ProductsFacadeApi.Domain.Entities;

namespace ProductsFacadeApi.Infrastructure.Dto
{
    public class ProductCacheDto
    {
        /// <summary>
        /// Список контактов
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Общее количество продуктов.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Текущий индекс страницы.
        /// </summary>
        public int PageIndex { get; set; }
    }
}
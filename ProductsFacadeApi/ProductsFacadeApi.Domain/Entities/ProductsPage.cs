using System.Collections.Generic;

namespace ProductsFacadeApi.Domain.Entities
{
    /// <summary>
    /// Страница товаров.
    /// </summary>
    public class ProductsPage
    {
        /// <summary>
        /// Список контактов
        /// </summary>
        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// Общее количество товаров.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
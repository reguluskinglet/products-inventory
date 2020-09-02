using System;

namespace ProductsFacadeApi.Infrastructure.Dto
{
    /// <summary>
    /// Dto товара.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание товара.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена товара.
        /// </summary>
        public decimal Price { get; set; }
    }
}
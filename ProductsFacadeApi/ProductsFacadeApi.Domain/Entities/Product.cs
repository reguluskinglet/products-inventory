using System;
using ProductsFacadeApi.DDD;

namespace ProductsFacadeApi.Domain.Entities
{
    /// <summary>
    /// Сущность товара.
    /// </summary>
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// Конуструктор сущности товара.
        /// </summary>
        internal Product() : base(Guid.NewGuid())
        {
        }
    }
}

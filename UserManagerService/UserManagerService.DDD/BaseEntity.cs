using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagerService.DDD
{
    /// <summary>
    /// Бозовая сущность.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public virtual Guid Id { get; protected set; }

        /// <summary>
        /// Конструктор. Нужен для маппинга.
        /// </summary>
        public BaseEntity()
        {
        }

        public BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
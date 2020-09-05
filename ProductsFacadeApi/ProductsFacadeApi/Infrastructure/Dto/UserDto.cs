using System;

namespace ProductsFacadeApi.Infrastructure.Dto
{
    /// <summary>
    /// Dto с информацией пользователя.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Признак того, что оператор сейчас залогинен
        /// </summary>
        public bool IsActive { get; set; }
    }
}
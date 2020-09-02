﻿using System;

namespace UserManagerService.Infrastructure.Dto
{
    /// <summary>
    /// Dto для пользователя
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
        /// Признак того, что user сейчас залогинен
        /// </summary>
        public bool IsActive { get; set; }
    }
}

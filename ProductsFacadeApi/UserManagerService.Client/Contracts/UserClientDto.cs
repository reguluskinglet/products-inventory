﻿using System;

namespace UserManagerService.Client.Contracts
{
    /// <summary>
    /// Dto пользователя
    /// </summary>
    public class UserClientDto
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

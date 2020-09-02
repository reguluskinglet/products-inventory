﻿using System;
using Microsoft.AspNetCore.Identity;

namespace UserManagerService.Domain.Entities
{
    /// <summary>
    /// Расширенная модель пользователя
    /// </summary>
    public class User : IdentityUser<Guid>
    {
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
        /// Получить ФИО пользователя
        /// </summary>
        public string GetFullName()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }

        public bool IsActive { get; set; }
    }
}

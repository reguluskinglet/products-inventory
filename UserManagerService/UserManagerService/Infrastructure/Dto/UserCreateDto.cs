﻿namespace UserManagerService.Infrastructure.Dto
{
    /// <summary>
    /// Dto для добавления пользователя
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

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
    }
}

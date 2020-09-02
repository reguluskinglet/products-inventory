﻿namespace UserManagerService.Infrastructure.Dto
{
    /// <summary>
    /// Dto для параметров авторизации
    /// </summary>
    public class LoginHeadersDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Режим получения авторизации
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// Идентификатор клиента приложения
        /// </summary>
        public string ClientId { get; set; }
    }
}

﻿namespace UserManagerService.Client.Contracts
{
    /// <summary>
    /// Заголовки для авторизации
    /// </summary>
    public class LoginHeadersClientDto
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

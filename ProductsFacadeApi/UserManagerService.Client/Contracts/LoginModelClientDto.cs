namespace UserManagerService.Client.Contracts
{
    /// <summary>
    /// Контракт для отправки данных авторизации сервису.
    /// </summary>
    public class LoginModelClientDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
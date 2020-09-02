using System;

namespace UserManagerService.Infrastructure.Dto
{
    /// <summary>
    /// Dto для результата добавления пользователя
    /// </summary>
    public class UserCreateResultDto
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
    }
}

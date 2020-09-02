using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsFacadeApi.ApplicationServices;
using ProductsFacadeApi.Infrastructure.Dto;

namespace ProductsFacadeApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями.
    /// </summary>
    [Authorize]
    [Route("api/[controller]"), ApiController]
    public class UsersController : Controller
    {
        private readonly ILogger _logger;
        private readonly UsersService _usersService;

        /// <summary>
        /// Создать
        /// </summary>
        public UsersController(UsersService usersService)
        {
            //_logger = logger;
            _usersService = usersService;
        }
        
        /// <summary>
        /// Авторизироваться в системе и получить токен
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Login))
            {
                var message = "Электронная почта не указана.";
                //_logger.LogWarning(message);
                return BadRequest(message);
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                var message = "Пароль не указан";
                //_logger.LogWarning(message);
                return BadRequest(message);
            }

            Result<string> tokenResult = await _usersService.GetToken(model);
            if (tokenResult.IsFailure)
            {
                var message = "Ошибка получения токена.";
                //_logger.LogWarning(message);
                return BadRequest(message);
            }

            return Ok(tokenResult.Value);
        }
    }
}
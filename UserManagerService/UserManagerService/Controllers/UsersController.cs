﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagerService.ApplicationServices;
using UserManagerService.Client.Contracts;
using UserManagerService.Infrastructure.Dto;

namespace Sphaera.UserManagement.Controllers
{
    /// <summary>
    /// Методы для работы с пользователями.
    /// </summary>
    [Route("api/[controller]"), ApiController]
    public class UsersController : Controller
    {
        private readonly UsersService _userService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор для инъекции зависимостей
        /// </summary>
        public UsersController(
            UsersService userService, 
            IMapper mapper)
        {
            //_logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить токен авторизации
        /// </summary>
        [HttpPost]
        [Route("getToken")]
        public async Task<ActionResult<string>> GetTokenAsync([FromBody] LoginHeadersClientDto loginHeadersClientDto)
        {
            string redirectUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/connect/token";

            var loginHeadersDto = _mapper.Map<LoginHeadersClientDto, LoginHeadersDto>(loginHeadersClientDto);

            Result<string> tokenResult = await _userService.GetToken(loginHeadersDto, redirectUrl);

            return Ok(tokenResult.Value);
        }

        /// <summary>
        /// Получить пользователя по его идентификатору
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserClientDto>> GetUserById(Guid userId)
        {
            if (userId == default)
            {
                var message = $"{nameof(userId)} не установлен.";
                //_logger.LogWarning(message);
                return BadRequest(message);
            }

            Result<UserDto> userResult = await _userService.GetUserById(userId);

            if (userResult.IsFailure)
            {
                return BadRequest("Пользователь не найден.");
            }

            return Ok(_mapper.Map<UserDto, UserClientDto>(userResult.Value));
        }

        /// <summary>
        /// Выход текущего пользователя из системы.
        /// </summary>
        [HttpPost("logout/{userId}")]
        public async Task<ActionResult> Logout(Guid userId)
        {
            if (userId == default)
            {
                var message = $"{nameof(userId)} не установлен.";
                _logger.LogWarning(message);
                return BadRequest(message);
            }

            var userResult = await _userService.Logout(userId);
            if (userResult.IsFailure)
            {
                _logger.LogWarning(userResult.Error);
                return BadRequest(userResult.Error);
            }

            return Ok(userResult.IsSuccess);
        }
    }
}

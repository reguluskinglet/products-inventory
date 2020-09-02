﻿using System;
using System.Collections.Generic;
using System.Linq;
 using System.Net;
 using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using UserManagerService.DDD;
using UserManagerService.Domain.Entities;
using UserManagerService.Infrastructure.Dto;

 namespace UserManagerService.ApplicationServices
{
    /// <summary>
    /// Сервис для управления пользователями
    /// </summary>
    public class UsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// http клиент который необходимо использовать для запросов
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <inheritdoc />
        public UsersService(
            IHttpClientFactory httpFactory,
            IMapper mapper,
            UnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            //_logger = logger;
            _mapper = mapper;
            HttpClient = httpFactory.CreateClient();
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Добавить пользователя с настройками по умолчанию
        /// </summary>
        public async Task<Result<UserCreateResultDto>> Create(UserCreateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var identityResult = await _userManager.CreateAsync(user, userDto.Password);
            if (!identityResult.Succeeded)
            {
                var message =
                    $"Ошибка создания пользователя. {string.Join("; ", identityResult.Errors.Select(e => e.Description))}";
                //_logger.LogWarning(message);
                return Result.Failure<UserCreateResultDto>(message);
            }

            var userResult = new UserCreateResultDto { Id = user.Id};

            return Result.Success(userResult);
        }

        /// <summary>
        /// Получить пользователя по Id
        /// </summary>
        public async Task<Result<UserDto>> GetUserById(Guid userId)
        {
            var userResult = await FindUserById(userId.ToString());
            if (userResult.IsFailure)
            {
                var message = $"Ошибка при получении пользователя";
                //_logger.LogWarning(message);
                return Result.Failure<UserDto>(message);
            }
            var user = userResult.Value;

            var userDto = _mapper.Map<UserDto>(user);

            return Result.Success(userDto);
        }

        /// <summary>
        /// Получить токен
        /// </summary>
        public async Task<Result<string>> GetToken(LoginHeadersDto loginHeadersDto, string redirectUrl)
        {
            var requestData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", loginHeadersDto.GrantType),
                new KeyValuePair<string, string>("client_id", loginHeadersDto.ClientId),
                new KeyValuePair<string, string>("username", loginHeadersDto.Login),
                new KeyValuePair<string, string>("password", loginHeadersDto.Password)
            };

            var response = await HttpClient.PostAsync(redirectUrl, new FormUrlEncodedContent(requestData));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Result.Success(responseContent);
            }
            else
            {
                var message = "Ошибка получения токена.";
                //_logger.LogWarning(message);
                return Result.Failure<string>(message);
            }
        }

        /// <summary>
        /// Разлогинивание пользователя
        /// </summary>
        public async Task<Result> Logout(Guid userId)
        {
            var userResult = await FindUserById(userId.ToString());
            if (userResult.IsFailure)
            {
                var message = "Пользователь не найден..";
                //_logger.LogWarning(message);
                return Result.Failure(message);
            }
            var user = userResult.Value;

            await _signInManager.SignOutAsync();

            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Result.Success();
            }

            return Result.Failure("Ошибка разлогина.");
        }

        private async Task<Result<User>> FindUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var message = $"Юзер с Id {userId} не найден.";
                //_logger.LogWarning(message);
                return Result.Failure<User>(message);
            }

            return Result.Success(user);
        }
    }
}

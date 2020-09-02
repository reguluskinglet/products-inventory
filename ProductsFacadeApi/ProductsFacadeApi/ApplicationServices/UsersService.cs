using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using ProductsFacadeApi.Infrastructure.Dto;
using UserManagerService.Client;
using UserManagerService.Client.Contracts;

namespace ProductsFacadeApi.ApplicationServices
{
    /// <summary>
    /// Сервис пользователей.
    /// </summary>
    public class UsersService
    {
        private readonly ILogger _logger;
        private readonly UserManagerServiceClient _userManagerServiceClient;

        public UsersService(UserManagerServiceClient userManagerServiceClient)
        {
            //_logger = logger;
            _userManagerServiceClient = userManagerServiceClient;
        }

        /// <summary>
        /// Получить токен авторизации
        /// </summary>
        /// <returns></returns>
        public async Task<Result<string>> GetToken(LoginDto model)
        {
            Result<string> tokenResult = await _userManagerServiceClient.GetToken(new LoginModelClientDto
            {
                Login = model.Login,
                Password = model.Password
            });

            if (tokenResult.IsFailure)
            {
                var message = "Ошибка при получении токена авторизации.";
                //_logger.LogWarning(message);
                return Result.Failure<string>(message);
            }

            return Result.Success(tokenResult.Value);
        }
    }
}
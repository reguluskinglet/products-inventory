using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Authorization;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProductsFacadeApi.Authorization.Contexts;
using UserManagerService.Client.Contracts;
using UserManagerService.Client.Options;

namespace UserManagerService.Client
{
    /// <summary>
    /// Клиент для обращения к UserManagerService.
    /// </summary>
    public sealed class UserManagerServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly string _userManagerUri;
        
        public UserManagerServiceClient(
            IHttpClientFactory httpFactory,
            IOptions<UserManagerServiceOptions> userMangerServiceOptions)
        {
            //_logger = logger;
            _httpClient = httpFactory.CreateClient();
            _userManagerUri = $"http://localhost:5002/api/";

            if (!AuthorizationContext.CurrentUserId.HasValue)
                return;

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserId, AuthorizationContext.CurrentUserId.ToString());
        }
        
        /// <summary>
        /// Получить токен авторизации.
        /// </summary>
        public async Task<Result<string>> GetToken(LoginModelClientDto loginModelClientDto)
        {
            //_logger.LogDebug($"Начало получения токена от {loginModelClientDto.Login}.");

            var url = $"{_userManagerUri}users/getToken";
            var data = new LoginHeadersClientDto
            {
                Login = loginModelClientDto.Login,
                Password = loginModelClientDto.Password,
                GrantType = "password",
                ClientId = "browser"
            };

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync<LoginHeadersClientDto>(url, data);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }
    }
}
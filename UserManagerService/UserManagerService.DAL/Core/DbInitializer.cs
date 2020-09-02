using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserManager.DAL.Migrations.Metadata;
using UserManagerService.Domain;
using UserManagerService.Domain.Entities;

 namespace UserManagerService.DAL.Core
{
    /// <summary>
    /// Класс для инициализации и заполнения данных пользователей
    /// </summary>
    public class DbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<ConfigurationManager> _configurationManager;
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly IdentityServerConfig _identityServerConfig;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configurationManager"></param>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="configurationDbContext"></param>
        /// <param name="identityServerConfig"></param>
        /// <param name="logger"></param>
        public DbInitializer(IOptions<ConfigurationManager> configurationManager,
            UserManager<User> userManager,
            ConfigurationDbContext configurationDbContext,
            IdentityServerConfig identityServerConfig
            )
        {
            _userManager = userManager;
            _configurationManager = configurationManager;
            _configurationDbContext = configurationDbContext;
            _identityServerConfig = identityServerConfig;
            //_logger = logger;
        }

        /// <summary>
        /// Создания необходимых БД и выполнение миграций. 
        /// </summary>
        public async Task Initialize()
        {
            await SeedUsers();

            if (!_configurationDbContext.Clients.Any())
            {
                foreach (var client in _identityServerConfig.GetClients().ToList())
                {
                    _configurationDbContext.Clients.Add(client.ToEntity());
                }

                _configurationDbContext.SaveChanges();
            }

            if (!_configurationDbContext.IdentityResources.Any())
            {
                foreach (var resource in _identityServerConfig.GetIdentityResources().ToList())
                {
                    _configurationDbContext.IdentityResources.Add(resource.ToEntity());
                }

                _configurationDbContext.SaveChanges();
            }

            if (!_configurationDbContext.ApiResources.Any())
            {
                foreach (var resource in _identityServerConfig.GetApiResources().ToList())
                {
                    _configurationDbContext.ApiResources.Add(resource.ToEntity());
                }

                _configurationDbContext.SaveChanges();
            }
        }

        private async Task SeedUsers()
        {
            //_logger.LogDebug("Начать создание пользователей");

            await AddUserIfNotExists(new User
            {
                Id = UsersMetadata.TestUserOne,
                UserName = UsersMetadata.UserLoginOne,
                MiddleName = UsersMetadata.MiddleNameUserOne,
                FirstName = UsersMetadata.FirstNameUserOne,
                LastName = UsersMetadata.LastNameUserOne,
                Email = UsersMetadata.UserLoginOne
            });

            await AddUserIfNotExists(new User
            {
                Id = UsersMetadata.TestUserTwo,
                UserName = UsersMetadata.UserLoginTwo,
                MiddleName = UsersMetadata.MiddleNameUserTwo,
                FirstName = UsersMetadata.FirstNameUserTwo,
                LastName = UsersMetadata.LastNameUserTwo,
                Email = UsersMetadata.UserLoginTwo
            });

            //_logger.LogInformation("Пользователи добавлены в Бд успешно.");
        }

        private async Task AddUserIfNotExists(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (existingUser == null)
            {
                await _userManager.CreateAsync(user, _configurationManager.Value.DefaultAdminPassword);
            }
        }
    }
}

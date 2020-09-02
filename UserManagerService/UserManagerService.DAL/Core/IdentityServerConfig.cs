﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using UserManagerService.Domain;

namespace UserManagerService.DAL.Core
{
    /// <summary>
    /// Конфигурация для настройки клиента и ресурсов
    /// </summary>
    public class IdentityServerConfig
    {
        readonly IOptions<ConfigurationManager> _configurationManager;

        /// <inheritdoc />
        public IdentityServerConfig(IOptions<ConfigurationManager> configurationManager)
        {
            _configurationManager = configurationManager;
        }

        /// <summary>
        /// Настройки информации для клиентских приложений. Определяет, какие scopes будут доступны IdentityServer
        /// </summary>
        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        /// <summary>
        /// Настройки информации для API. Определяет, какие scopes будут доступны IdentityServer
        /// </summary>
        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
                new ApiResource(_configurationManager.Value.ApiName, _configurationManager.Value.ApiName)
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Email,
                    }
                }
            };
        }

        /// <summary>
        /// Получение клиентских приложений
        /// </summary>
        public IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = _configurationManager.Value.ClientId,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        _configurationManager.Value.ApiName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}

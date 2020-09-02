﻿namespace UserManagerService.Domain
{
    /// <summary>
    /// Конфигурация для IdentityServer
    /// </summary>
    public class ConfigurationManager
    {
        public string Hash { get; set; }

        public string AppSecret { get; set; }
        public string DefaultAdminPassword { get; set; }
        public string ClientId { get; set; }
        public string ApiName { get; set; }
    }
}

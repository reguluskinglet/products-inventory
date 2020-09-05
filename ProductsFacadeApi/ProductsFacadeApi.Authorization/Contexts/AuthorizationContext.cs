﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("Sphaera.OperatorFacadeAPI.Service.Tests")]
[assembly: InternalsVisibleTo("Sphaera.CallManagement.Tests")]
[assembly: InternalsVisibleTo("Sphaera.InboxDistribution.IntegrationTests")]

namespace ProductsFacadeApi.Authorization.Contexts
{
    /// <summary>
    /// Информация о текущем пользователе
    /// </summary>
    public static class AuthorizationContext
    {
        private static readonly AsyncLocal<Guid?> currentUserIdStorage = new AsyncLocal<Guid?>();

        /// <summary>
        /// Идентификатор текущего пользователя.
        /// </summary>
        public static Guid? CurrentUserId
        {
            get => currentUserIdStorage.Value;
            set => currentUserIdStorage.Value = value;
        }
    }
}

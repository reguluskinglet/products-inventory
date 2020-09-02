﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Authorization;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ProductsFacadeApi.Authorization.Contexts;

namespace ProductsFacadeApi.Authorization.Middlewares
{
    /// <summary>
    /// Middleware для заполнения идентификатора текущего пользователя
    /// </summary>
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Создает новый экземпляр <see cref="AuthorizationMiddleware"/>.
        /// </summary>
        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userIdStr = GetUserId(context);

            if (Guid.TryParse(userIdStr, out Guid userId))
            {
                AuthorizationContext.CurrentUserId = userId;
            }
            else
            {
                AuthorizationContext.CurrentUserId = null;
            }

            await _next.Invoke(context);
        }

        private string GetUserId(HttpContext context)
        {
            var userIdFromToken = GetUserIdFromToken(context);
            if (!string.IsNullOrWhiteSpace(userIdFromToken))
            {
                return userIdFromToken;
            }

            var userIdFromHeader = GetUserIdFromHeader(context);
            if (!string.IsNullOrWhiteSpace(userIdFromHeader))
            {
                return userIdFromHeader;
            }

            return null;
        }

        private string GetUserIdFromToken(HttpContext context)
        {
            var user = context.User;
            var userId = user.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject)?.Value;
            return userId;
        }

        private string GetUserIdFromHeader(HttpContext context)
        {
            context.Request.Headers.TryGetValue(HeaderNames.UserId, out StringValues userId);

            return userId;
        }
    }
}

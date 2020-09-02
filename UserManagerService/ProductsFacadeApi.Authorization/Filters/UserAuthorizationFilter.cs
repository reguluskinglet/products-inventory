﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductsFacadeApi.Authorization.Contexts;

namespace ProductsFacadeApi.Authorization.Filters
{
    /// <summary>
    /// Фильтр авторизации пользователя
    /// </summary>
    public class UserAuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public UserAuthorizationFilter()
        {
        }

        /// <summary>
        /// Проверка, считаем ли пользователя авторизованным
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (AuthorizationContext.CurrentUserId.HasValue == false)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }
        }
    }
}

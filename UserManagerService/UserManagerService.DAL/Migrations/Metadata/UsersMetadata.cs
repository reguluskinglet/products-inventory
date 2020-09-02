﻿using System;

namespace UserManager.DAL.Migrations.Metadata
{
    public static class UsersMetadata
    {
        #region UserIds

        /// <summary>
        /// Id Пользователя
        /// </summary>
        public static Guid TestUserOne = new Guid("296eddc7-e248-4246-9fdb-000000000000");

        /// <summary>
        /// Id Пользователя
        /// </summary>
        public static Guid TestUserTwo = new Guid("296eddc7-e248-4246-9fdb-000000000001");

        #endregion

        #region UserLogins

        /// <summary>
        /// Login Пользователя
        /// </summary>
        public static string UserLoginOne = "test@one.com";

        /// <summary>
        /// Id Пользователя
        /// </summary>
        public static string UserLoginTwo = "test@two.com";

        #endregion

        #region Extensions

        /// <summary>
        /// First Name Пользователя
        /// </summary>
        public const string FirstNameUserOne = "Иванов";

        /// <summary>
        /// Last Name Пользователя
        /// </summary>
        public const string LastNameUserOne = "Иван";

        /// <summary>
        /// Middle Name Пользователя
        /// </summary>
        public const string MiddleNameUserOne = "Иванович";

        /// <summary>
        /// First Name Пользователя
        /// </summary>
        public const string FirstNameUserTwo = "Петров";

        /// <summary>
        /// Last Name Пользователя
        /// </summary>
        public const string LastNameUserTwo = "Петр";

        /// <summary>
        /// Middle Name Пользователя
        /// </summary>
        public const string MiddleNameUserTwo = "Петрович";

        #endregion
    }
}

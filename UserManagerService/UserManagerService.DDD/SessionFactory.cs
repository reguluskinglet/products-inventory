﻿using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;
using NHibernate;

namespace UserManagerService.DDD
{
    /// <summary>
    /// Фабрика сессий.
    /// </summary>
    public class SessionFactory : IDisposable
    {
        private readonly ISessionFactory _factory;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        /// <param name="assembly"></param>
        public SessionFactory(string connectionString, Assembly assembly)
        {
            _factory = BuildSessionFactory(connectionString, assembly);
        }

        /// <summary>
        /// Открыть новую сессию.
        /// </summary>
        /// <returns>Сессия.</returns>
        internal ISession OpenSession()
        {
            return _factory.OpenSession();
        }

        private static ISessionFactory BuildSessionFactory(string connectionString, Assembly assembly)
        {
            var configuration = Fluently.Configure()
                .Database(
                    PostgreSQLConfiguration.PostgreSQL82.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(assembly)
                    .Conventions.Add(
                        ForeignKey.EndsWith("id"),
                        ConventionBuilder.Property.When(
                            criteria => criteria.Expect(
                                x => x.Nullable,
                                Is.Not.Set),
                            x => x.Not.Nullable()))
                    .Conventions.Add<OtherConversions>()
                );

            return configuration.BuildSessionFactory();
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _factory?.Dispose();
        }

        /// <summary>
        /// Конверсии.
        /// </summary>
        private class OtherConversions : IHasManyConvention, IReferenceConvention
        {
            /// <summary>
            /// Apply changes to the target
            /// </summary>
            public void Apply(IOneToManyCollectionInstance instance)
            {
                instance.LazyLoad();
                instance.AsBag();
                instance.Cascade.SaveUpdate();
                instance.Inverse();
            }

            /// <summary>
            /// Apply changes to the target
            /// </summary>
            public void Apply(IManyToOneInstance instance)
            {
                instance.LazyLoad(Laziness.Proxy);
                instance.Cascade.None();
                instance.Not.Nullable();
            }
        }
    }
}
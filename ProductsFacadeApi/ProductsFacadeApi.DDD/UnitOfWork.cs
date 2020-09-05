using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using Microsoft.Extensions.Logging;

namespace ProductsFacadeApi.DDD
{
    /// <summary>
    /// Реализация паттерна Unit Of Work.
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private readonly SessionFactory _sessionFactory;
        private bool _isAlive;
        private ISession _session;
        private ITransaction _transaction;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sessionFactory">Фабрика сессий.</param>
        /// <param name="logger"></param>
        public UnitOfWork(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            //_logger = logger;
        }

        /// <summary>
        /// Реализация IDisposable.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Начать работу.
        /// </summary>
        /// <param name="isolationLevel">Уровень изоляции в котором будетм работать Unit Of Work.</param>
        /// <param name="riseEvents"> Включает трекинг и отправку событий</param>
        public UnitOfWork Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, bool riseEvents = true)
        {
            if (_isAlive)
            {
                return this;
            }

            //_logger.LogInformation($"Вызов метода {nameof(Begin)} в {nameof(UnitOfWork)}");

            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction(isolationLevel);
            _isAlive = true;

            return this;
        }

        /// <summary>
        /// Успешное завершение работы.
        /// </summary>
        public async Task CommitAsync()
        {
            if (!_isAlive)
            {
                return;
            }

            //_logger.LogInformation($"Вызов метода {nameof(Commit)} в {nameof(UnitOfWork)}");

            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                _session.Dispose();
                _sessionFactory.Dispose();
            }
        }

        /// <summary>
        /// Запрос к хранилищу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Результат запроса.</returns>
        public IQueryable<TEntity> Query<TEntity>()
        {
            return _session.Query<TEntity>();
        }

        /// <summary>
        /// Удаление сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        public async Task DeleteAsync<TEntity>(TEntity entity)
        {
            await _session.DeleteAsync(entity);
        }

        /// <summary>
        /// Получить объект по GUID.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Сущность.</returns>
        public async Task<TEntity> GetAsync<TEntity, TId>(TId id) where TEntity : class
        {
            return await _session.GetAsync<TEntity>(id);
        }

        /// <summary>
        /// Сохранение сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        internal async Task SaveOrUpdateAsync<TEntity>(TEntity entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isAlive)
            {
                return;
            }

            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                _session.Dispose();
                _sessionFactory.Dispose();
            }
        }
    }
}
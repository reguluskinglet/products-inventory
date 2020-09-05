using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsFacadeApi.DDD
{
    /// <summary>
    /// Интерфейс репозитория.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<IList<TEntity>> GetAllAsync();
    }
}
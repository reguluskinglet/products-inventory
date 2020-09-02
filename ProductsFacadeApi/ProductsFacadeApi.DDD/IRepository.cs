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
        Task Add(TEntity entity);

        Task<TEntity> GetById(Guid id);

        Task Delete(Guid id);

        Task<IList<TEntity>> GetAll();
    }
}
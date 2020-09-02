using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsFacadeApi.DDD
{
    public abstract class Repository <T> : IRepository<T> where T : BaseEntity
    {
        private readonly UnitOfWork _unitOfWork;

        protected Repository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Добавление сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public async Task Add(T entity)
        {
            await _unitOfWork.SaveOrUpdateAsync(entity);
        }

        /// <summary>
        /// Получение сущности по ID.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public async Task<T> GetById(Guid id)
        {
            return await _unitOfWork.GetAsync<T, Guid>(id);
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Сущность для удаления</param>
        public async Task DeleteAsync(T entity)
        {
            await _unitOfWork.DeleteAsync(entity);
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        /// <summary>
        /// Получить все сущности
        /// </summary>
        public async Task<IList<T>> GetAll()
        {
            return await Task.FromResult(_unitOfWork.Query<T>().ToList());
        }
    }
}
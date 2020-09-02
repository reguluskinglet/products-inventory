using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsFacadeApi.Domain.Entities;

namespace ProductsFacadeApi.DAL.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория товаров.
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        /// Получение постраничного списка товаров.
        /// </summary>
        /// <returns></returns>
        Task<ProductsPage> GetProductsPageAsync(int pageSize, int pageIndex);
    }
}

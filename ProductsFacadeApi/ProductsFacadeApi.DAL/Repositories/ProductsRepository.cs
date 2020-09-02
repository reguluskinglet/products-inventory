using System.Linq;
using System.Threading.Tasks;
using ProductsFacadeApi.DAL.Abstractions;
using ProductsFacadeApi.DDD;
using ProductsFacadeApi.Domain.Entities;

namespace ProductsFacadeApi.DAL.Repositories
{
    /// <summary>
    /// Реализация репозитория продуктов.
    /// </summary>
    public sealed class ProductsRepository : Repository<Product>, IProductsRepository
    {
        private readonly UnitOfWork _unitOfWork;

        /// <inheritdoc />
        public ProductsRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<ProductsPage> GetProductsPageAsync(int pageSize, int pageIndex)
        {
            var query = _unitOfWork.Query<Product>();
            var products = query
                .OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var productsPage = new ProductsPage()
            {
                Products = products,
                TotalCount = query.Count()
            };

            return await Task.FromResult(productsPage);
        }
    }
}
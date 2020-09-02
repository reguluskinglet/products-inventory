using FluentNHibernate.Mapping;
using ProductsFacadeApi.Domain.Entities;

namespace ProductsFacadeApi.DAL.Mappings
{
    /// <summary>
    /// Маппинг сущности ProductsMap в таблицу.
    /// </summary>
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("products");
            Id(x => x.Id);
            Map(x => x.Title, "title").Nullable();
            Map(x => x.Description, "description").Nullable();
            Map(x => x.Price, "price").Nullable();

            Not.LazyLoad();
        }
    }
}
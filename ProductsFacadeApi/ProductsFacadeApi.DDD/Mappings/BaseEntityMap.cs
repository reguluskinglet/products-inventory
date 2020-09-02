﻿using FluentNHibernate.Mapping;

namespace ProductsFacadeApi.DDD.Mappings
{
    /// <summary>
    /// Базовый маппинг для всех маппингов сущностей наследуемых от BaseEntity
    /// </summary>
    public class BaseEntityMap<T> : ClassMap<T> where T : BaseEntity
    {
        /// <inheritdoc />
        public BaseEntityMap()
        {
            Id(x => x.Id, "id").GeneratedBy.Assigned();
            Not.LazyLoad();
        }
    }
}
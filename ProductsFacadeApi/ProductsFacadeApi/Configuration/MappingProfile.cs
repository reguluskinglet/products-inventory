﻿using AutoMapper;
using ProductsFacadeApi.Domain.Entities;
using ProductsFacadeApi.Infrastructure.Dto;

namespace ProductsFacadeApi.Configuration
{
    /// <summary>
    /// Настройка маппинга для AutoMapper
    /// Подтягивается автоматически автомаппером.
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class MappingProfile : Profile
    {
        /// <inheritdoc />
        public MappingProfile()
        {
            CreateMap<ProductsPage, ProductsPageDto>();
        }
    }
}

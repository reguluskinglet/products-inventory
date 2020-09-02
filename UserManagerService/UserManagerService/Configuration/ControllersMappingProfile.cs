﻿using AutoMapper;
using UserManagerService.Client.Contracts;
using UserManagerService.Infrastructure.Dto;

namespace UserManagerService.Configuration
{
    /// <summary>
    /// Профиль для маппинга
    /// </summary>
    public class ControllersMappingProfile : Profile
    {
        /// <inheritdoc />
        public ControllersMappingProfile()
        {
            CreateMap<LoginHeadersClientDto, LoginHeadersDto>();
            CreateMap<UserDto, UserClientDto>();
        }
    }
}

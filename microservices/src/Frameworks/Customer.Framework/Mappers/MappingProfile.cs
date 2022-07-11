namespace Customer.Framework.Mappers
{
    using AutoMapper;
    using System;
    using Customer.Framework.Data.Entities;
    using Customer.Framework.Extensions;
    using Customer.Framework.Domain;
    using Customer.Framework.Domain.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerModel, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

              }
    }
}

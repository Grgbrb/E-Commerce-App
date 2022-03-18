using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product,ProductDto>()
            .ForMember(d => d.ProductBrand, x => x.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, x => x.MapFrom(s => s.ProductType.Name))
            .ForMember(p => p.PictureUrl, x => x.MapFrom<ProductUrlResolver>());
        }
    }
}
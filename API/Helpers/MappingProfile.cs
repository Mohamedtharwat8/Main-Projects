using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<Product, ProductToReturnDto>()
            //     .ForMember(u => u.ProductBrand, opt => opt.MapFrom(x => x.ProductBrand.Name))
            //     .ForMember(u => u.ProductType, opt => opt.MapFrom(x => x.ProductType.Name));

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}

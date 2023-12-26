using AutoMapper;
using Cms.Application.Admin.Models.Product;
using Cms.Infrastructure.Persistence.Entities;
using Configuration.Extensions;

namespace Cms.Application.Admin.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>()
            .IgnoreAllNonExisting();
    }
}
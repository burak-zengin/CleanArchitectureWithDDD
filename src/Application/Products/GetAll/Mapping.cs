using AutoMapper;

namespace Application.Products.GetAll;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Domain.Products.Product, Product>()
            .ForMember(_ => _.Id, _ => _.MapFrom(_ => _.Id.Value))
            .ForMember(_ => _.ModelCode, _ => _.MapFrom(_ => _.ModelCode.Value));

        CreateMap<Domain.Products.ProductItem, ProductItem>()
            .ForMember(_ => _.Id, _ => _.MapFrom(_ => _.Id.Value))
            .ForMember(_ => _.Currency, _ => _.MapFrom(_ => _.Price.Currency))
            .ForMember(_ => _.Price, _ => _.MapFrom(_ => _.Price.Amount));
    }
}

using AutoMapper;
using Common.ViewModel;
using Core.Model;

namespace ApiCRUD.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEdit, Product>().ReverseMap();
        }
    }
}

using AutoMapper;
using Model.Entites;
using Model.ViewModel;

namespace Model.AutoMapper
{
    public class MappingProfile : Profile
    {
        // setup mapping trực tiếp giữa  các entityViewModel và entityModel và ngược lại
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}
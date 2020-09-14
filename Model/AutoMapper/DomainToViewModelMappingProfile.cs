using AutoMapper;
using Model.Entites;
using Model.ViewModel;

namespace Model.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        // setup mapping trực tiếp giữa  các entityViewModel và entityModel
        public DomainToViewModelMappingProfile()
        {
             CreateMap<Product, ProductViewModel>();
        }
    }
}
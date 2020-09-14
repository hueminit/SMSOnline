using AutoMapper;
using System;
using Model.Entites;
using Model.ViewModel;

namespace Model.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(c => new Product(c.Id, c.Name));  
        }
    }
}

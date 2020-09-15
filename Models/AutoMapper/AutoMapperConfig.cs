using AutoMapper;
using Models.Entities;
using Models.ViewModel;

namespace Models.AutoMapper
{
    public class AutoMapperConfig 
    {
       public static IMapper Mapper { get; private set; }
        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUser, AppUserViewModel>().ReverseMap();
                cfg.CreateMap<AppUserViewModel, AppUser>().ReverseMap();

                cfg.CreateMap<Test, TestViewModel>().ReverseMap();
                cfg.CreateMap<TestViewModel, Test>().ReverseMap();
            });

            Mapper = config.CreateMapper();
        }
    }
}
using AutoMapper;
using Data.Helpers;
using Data.Infrastructure;
using Microsoft.AspNet.Identity;
using Models.AutoMapper;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{


    public interface IUserService : IRepository<Test>
    {
        //Task<string> FindUserByEmailOrUserName(string userOrName);
        Task<bool> Save();
    }

    public class UserService : RepositoryBase<Test>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(IDbFactory dbFactory, IUnitOfWork unitOfWork, 
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
            _userManager = userManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public Task<bool> Save()
        {
            return _unitOfWork.Commit();
        }
    }
}

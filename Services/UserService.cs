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
using Microsoft.AspNet.Identity.EntityFramework;
using Models.ViewModel;

namespace Services
{
    public interface IUserService : IRepository<AppUser>
    {
        Task<AppUserViewModel> FindUserByEmailOrUserNameOrPhoneNumber(CheckAccountViewModel model);
        Task<bool> Save();
    }

    public class UserService : RepositoryBase<AppUser>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
        }

        public async Task<AppUserViewModel> FindUserByEmailOrUserNameOrPhoneNumber(CheckAccountViewModel model)
        {

            var user = await GetSingleByCondition(x => x.Email == model.Email
                                                     || x.UserName == model.UserName || x.PhoneNumber == model.PhoneNumber);
            if (user != null)
            {
                return _mapper.Map<AppUser, AppUserViewModel>(user);
            }

            return null;
        }

        public Task<bool> Save()
        {
            return _unitOfWork.Commit();
        }
    }
}

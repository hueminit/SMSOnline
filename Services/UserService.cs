using AutoMapper;
using Data.Helpers;
using Data.Infrastructure;
using Microsoft.AspNet.Identity;
using Models.AutoMapper;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.ViewModel;
using Models.ViewModel.Others;

namespace Services
{
    public interface IUserService : IRepository<AppUser>
    {
        Task<AppUserViewModel> FindUserByEmailOrUserNameOrPhoneNumber(CheckAccountViewModel model);
        Task<List<AppUserViewModel>> FindUser(string keyword);
        Task<AppUserViewModel> GetUserById(string userId);
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

        public async Task<List<AppUserViewModel>> FindUser(string keyword)
        {
            var query = await GetMulti(x => x.IsDelete == false
                                            && (x.FullName.Contains(keyword)
                                                || x.Email.Contains(keyword)
                                                || x.UserName.Contains(keyword)
                                                || x.PhoneNumber.Contains(keyword)));
            query = query.OrderByDescending(x => x.UserName);
            return await _mapper.ProjectTo<AppUserViewModel>(query).ToListAsync();
        }

        public async Task<AppUserViewModel> GetUserById(string userId)
        {
            var user = await GetSingleByCondition(x => x.Id == userId);

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

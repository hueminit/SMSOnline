using AutoMapper;
using Data.Helpers;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.Shared;
using Models.ViewModel;
using Models.ViewModel.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService : IRepository<AppUser>
    {
        Task<AppUserViewModel> FindUserByEmailOrUserNameOrPhoneNumber(CheckAccountViewModel model);

        Task<PaginationSet<AppUserViewModel>> FindUser(string currentUserId, string keyword, int page = 1, int pageSize = 8);

        Task<AppUserViewModel> GetUserByIdAsync(string userId, string currentUserId);

        AppUserViewModel GetUserById(string userId, string currentUserId);

        RequestFriendModel CheckRequestFriendModel(string currentUserId, string profileId);

        Task<bool> UpdateUser(AppUserViewModel user);

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
            var user = await GetSingleByConditionAsync(x => x.Email == model.Email
                                                     || x.UserName == model.UserName || x.PhoneNumber == model.PhoneNumber);
            if (user != null)
            {
                return _mapper.Map<AppUser, AppUserViewModel>(user);
            }

            return null;
        }

        public async Task<PaginationSet<AppUserViewModel>> FindUser(string currentUserId, string keyword, int page = 1, int pageSize = 8)
        {
            try
            {
                var query = await GetMultiAsync(x => x.IsDelete == false && x.Id != currentUserId);
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(x => x.FullName.Contains(keyword)
                                             || x.Email.Contains(keyword)
                                             || x.UserName.Contains(keyword)
                                             || x.PhoneNumber.Contains(keyword));
                }
                int totalRow = query.Count();
                var totalPage = (int)Math.Ceiling((decimal)totalRow / pageSize);
                query = query.OrderByDescending(x => x.DateCreated)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                var users = query.ToList();
                var data = _mapper.Map<List<AppUser>, List<AppUserViewModel>>(users);
                var updateData = data?.Select(
                    c =>
                    {
                        var request = CheckRequestFriendModel(currentUserId, c.Id);
                        c.IsFriendWithCurrentUser = request.IsFriend;
                        c.IsCurrentUserSendRequest = request.IsCurrentUserSendRequest;
                        c.StatustRequest = request.StatustRequest;
                        return c;
                    }).ToList();
                var res = new PaginationSet<AppUserViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = totalPage,
                    Items = updateData,
                    MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"))
                };
                return res;
            }
            catch (Exception e)
            {
                var test = e;
            }

            return null;
        }

        public async Task<AppUserViewModel> GetUserByIdAsync(string userId, string currentUserId)
        {
            var user = await GetSingleByConditionAsync(x => x.Id == userId);

            if (user != null)
            {
                var model = _mapper.Map<AppUser, AppUserViewModel>(user);
                var request = CheckRequestFriendModel(currentUserId, model.Id);
                model.IsCurrentUserSendRequest = request.IsCurrentUserSendRequest;
                model.StatustRequest = request.StatustRequest;
                model.IsFriendWithCurrentUser = request.IsFriend;
                return model;
            }

            return null;
        }

        public AppUserViewModel GetUserById(string userId, string currentUserId)
        {
            var user = GetSingleByCondition(x => x.Id == userId);

            if (user != null)
            {
                var model = _mapper.Map<AppUser, AppUserViewModel>(user);
                var request = CheckRequestFriendModel(currentUserId, model.Id);
                model.IsCurrentUserSendRequest = request.IsCurrentUserSendRequest;
                model.StatustRequest = request.StatustRequest;
                model.IsFriendWithCurrentUser = request.IsFriend;
                return model;
            }

            return null;
        }

        public RequestFriendModel CheckRequestFriendModel(string currentUserId, string profileId)
        {
            var model = new RequestFriendModel();
            var query = DbContext.Contacts.AsNoTracking();
            var contact = query.FirstOrDefault(x => (x.ContactReceivedId == profileId
                                                     && x.ContactSentId == currentUserId)
                                                    || (x.ContactReceivedId == currentUserId
                                                        && x.ContactSentId == profileId));
            if (contact != null)
            {
                if (contact.ContactSentId.Equals(currentUserId) && contact.ContactReceivedId.Equals(profileId))
                {
                    model.IsCurrentUserSendRequest = true;
                }
                model.IsFriend = contact.IsFriend;
                model.StatustRequest = contact.StatusRequest;
            }

            return model;
        }

        public async Task<bool> UpdateUser(AppUserViewModel user)
        {
            var model = _mapper.Map<AppUserViewModel, AppUser>(user);
            await Update(model);
            return true;
        }

        public Task<bool> Save()
        {
            return _unitOfWork.Commit();
        }
    }
}
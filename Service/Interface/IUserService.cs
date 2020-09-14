using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Utilities;
using Model.ViewModel;

namespace Services.Interface
{
    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task<bool> DeleteAsync(string id);

        Task<List<AppUserViewModel>> GetAllAsync();

        PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppUserViewModel> GetById(string id);

        Task<bool> ChangePassByUserAsync(AppUserViewModel userVm, string password);

        Task<bool> UpdateByUserAsync(AppUserViewModel userVm, string password);


        Task<bool> UpdateAsync(AppUserViewModel userVm);

        Task<bool> RemoveRolesFromUser(string userId, string[] roles);

        Task<bool> CheckPasswordUser(Guid? id);

        Task<AppUserViewModel> ChangePassByUserWithPasswordHashIsNull(AppUserViewModel userVm);

        Task<bool> FindUserByEmailOrUserName(string userOrName);
    }
}

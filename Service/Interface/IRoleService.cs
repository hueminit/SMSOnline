﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Utilities;

namespace Services.Interface
{
    public interface IRoleService
    {
        // thư viện usermanger chỉ dùng những phương thức dạng bất đồng bộ
        //Task<bool> AddAsync(AppRoleViewModel userVm);
        //Task<bool> AddAsync(AnnouncementViewModel announcement, List<AnnouncementUserViewModel> announcementUsers, AppRoleViewModel userVm);

        //Task DeleteAsync(Guid id);

        //Task<List<AppRoleViewModel>> GetAllAsync();

        //PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        //Task<AppRoleViewModel> GetById(Guid id);


        //Task UpdateAsync(AppRoleViewModel userVm);

        //List<PermissionViewModel> GetListFunctionWithRole(Guid roleId);

        //void SavePermission(List<PermissionViewModel> permissions, Guid roleId);

        //Task<bool> CheckPermission(string functionId, string action, string[] roles);

        //Task<bool> CheckRoleByUser(string userId);

        //Task<bool> CheckAccount(string emailorusername);
    }
}

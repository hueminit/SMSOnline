using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.Enums;
using Models.ViewModel;
using Models.ViewModel.Others;
using Services;
using SMSOnline.Helpers;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private string currentUser => IdentityHelper.CurrentUserId;
        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<ActionResult> Index(string profileId)
        {
            ViewBag.IsFriend = false;
            if (!string.IsNullOrWhiteSpace(profileId))
            {
                var user = await _userService.GetUserByIdAsync(profileId, currentUser);
                if (user != null)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Error", "Response", new { message = "Get profile failure" });
        }

        public async Task<ActionResult> Edit()
        {
            var user = await _userService.GetUserByIdAsync(currentUser, currentUser);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Error", "Response", new { message = "Get profile failure" });
        }
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "FullName,BirthDay,Email,Address,Gender,Description,PhoneNumber")] AppUserViewModel userViewModel)
        {
            var check = new CheckAccountViewModel()
            {
                Email = userViewModel.Email,
                PhoneNumber = userViewModel.PhoneNumber,
                UserName = IdentityHelper.CurrentUserName
            };

            var isUpdate = await _userService.CheckUpdateOrCreateUser(check);

            if (isUpdate)
            {
               
                HttpPostedFileBase fileUpload = Request.Files["fileUpload"];
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/uploads/"), Path.GetFileName(fileUpload.FileName));
                    fileUpload.SaveAs(path);
                    userViewModel.Avatar = "/Content/uploads/" + fileUpload.FileName;
                }
                var user = await _userService.GetUserByIdAsync(currentUser, currentUser);
                if (user != null)
                {
                    user.FullName = userViewModel.FullName;
                    user.BirthDay = userViewModel.BirthDay;
                    user.Email = userViewModel.Email;
                    user.Description = userViewModel.PhoneNumber;
                    user.Address = userViewModel.Address;
                    user.Gender = userViewModel.Gender;
                    user.Description = userViewModel.Description;
                    user.Description = user.UserName;
                    user.Avatar = string.IsNullOrWhiteSpace(userViewModel.Avatar) ? user.Avatar : userViewModel.Avatar;
                    await _userService.UpdateUser(user);
                    var res = await _userService.Save();
                    if (res)
                    {
                        return RedirectToAction("Index", "Profile", new { profileId = IdentityHelper.CurrentUserId });
                    }
                }
            }
            else
            {
                return View(userViewModel);

            }
            
            return RedirectToAction("Error", "Response", new { message = "Get profile failure" });
        }
    }
}
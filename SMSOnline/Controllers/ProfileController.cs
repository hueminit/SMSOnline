using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.ViewModel;
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
        public async Task<ActionResult> Edit(AppUserViewModel userViewModel, HttpPostedFileBase image)
        {
            var user = await _userService.GetUserByIdAsync(currentUser, currentUser);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Error", "Response", new { message = "Get profile failure" });
        }
    }
}
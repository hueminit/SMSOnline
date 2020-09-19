using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.ViewModel;
using Services;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactController(IContactService contactService, IUserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }
        public async Task<ActionResult> Index()
        {
            var currentUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var data = await _contactService.GetAllContact(true, currentUser);
            return View(data);
        }

        public async Task<ActionResult> Profile(string profileId)
        {
            ViewBag.IsFriend = false;
            if (!string.IsNullOrWhiteSpace(profileId))
            {
                var currentUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var user = await _userService.GetUserById(profileId, currentUser);
                if (user != null)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Error", "Response", new { message = "Get profile failure" });
        }

        [HttpPost]
        public async Task<ActionResult> AddContact(string profileId)
        {
            var currentUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = await _userService.GetUserById(profileId, currentUser);
            if (user != null)
            {
                var isSuccess = await _contactService.CreateContact(currentUser, user.Id);
                if (isSuccess)
                {
                    return RedirectToAction("Success", "Response", new { message = "Add friend successful" });
                }
            }
            return RedirectToAction("Error", "Response", new { message = "Add friend failure" });
        }

        public async Task<ActionResult> FindUser(string keyword, int page = 1)
        {
            var currentUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ViewBag.Keyword = keyword;
            var users = await _userService.FindUser(currentUser,keyword, page, 3);
            return View(users);
        }
    }
}
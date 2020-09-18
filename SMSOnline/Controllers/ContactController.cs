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

        public ActionResult Profile(string profileId)
        {
            if (!string.IsNullOrWhiteSpace(profileId))
            {
                var user = _userService.GetUserById(profileId);
                if (user != null)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult AddContact(string profileId)
        {
            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> FindUser(string keyword, int page = 1)
        {
            ViewBag.Keyword = keyword;
            var users = await _userService.FindUser(keyword, page, 3);
            return View(users);
        }
    }
}
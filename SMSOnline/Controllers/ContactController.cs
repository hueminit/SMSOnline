using Microsoft.AspNet.Identity;
using Services;
using SMSOnline.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;
        private string currentUser => IdentityHelper.CurrentUserId;

        public ContactController(IContactService contactService, IUserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }

        public async Task<ActionResult> Index(string keyword)
        {
            ViewBag.Keyword = keyword;
            var data = await _contactService.GetAllContact(keyword, true, currentUser);
            data.keyword = keyword;
            return View(data);
        }

        public async Task<ActionResult> RequestFriend(string keyword)
        {
            ViewBag.Keyword = keyword;
            var data = await _contactService.GetAllRequestFriend(keyword,currentUser);
            data.keyword = keyword;
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> AcceptRequest(string profileId)
        {
            var isSuccess = await _contactService.AcceptRequestFriend(currentUser, profileId);
            if (isSuccess)
            {
                return RedirectToAction("Success", "Response", new { message = "Accept request friend successful" });
            }
            return RedirectToAction("Error", "Response", new { message = "Accept request friend failure" });
        }

        [HttpPost]
        public async Task<ActionResult> CancelRequest(string profileId)
        {
            var isSuccess = await _contactService.CancelRequestFriend(currentUser, profileId);
            if (isSuccess)
            {
                return RedirectToAction("Success", "Response", new { message = "Cancel request friend successful" });
            }
            return RedirectToAction("Error", "Response", new { message = "Cancel request friend failure" });
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFriend(string profileId)
        {
            var isSuccess = await _contactService.CancelRequestFriend(currentUser, profileId);
            if (isSuccess)
            {
                return RedirectToAction("Success", "Response", new { message = "Remove friend successful" });
            }
            return RedirectToAction("Error", "Response", new { message = "Remove friend failure" });
        }


        [HttpPost]
        public async Task<ActionResult> AddContact(string profileId)
        {
            var user = await _userService.GetUserByIdAsync(profileId, currentUser);
            if (user != null)
            {
                var request = _userService.CheckRequestFriendModel(currentUser, user.Id);
                if (request.IsCurrentUserSendRequest && request.StatustRequest)
                {
                    return RedirectToAction("Error", "Response", new { message = "invitation has been sent" });
                }
                var userInfo = await _userService.GetUserByIdAsync(currentUser, currentUser);

                var isSuccess = await _contactService.CreateContact(currentUser, user, userInfo.FullName);
                if (isSuccess)
                {
                    return RedirectToAction("Success", "Response", new { message = "Add friend successful" });
                }
            }
            return RedirectToAction("Error", "Response", new { message = "Add friend failure" });
        }

        public async Task<ActionResult> FindUser(string keyword, int page = 1)
        {
            var users = await _userService.FindUser(currentUser, keyword, page, 3);
            users.keyword = keyword;
            return View(users);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.Enums;
using Models.ViewModel;
using Models.ViewModel.Others;
using Services;
using SMSOnline.Helpers;
using SMSOnline.Hub;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private string currentUser => IdentityHelper.CurrentUserId;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        public ChatController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }
        // GET: Chat
        public async Task<ActionResult> Index(string profileId)
        {
            ViewBag.Profile = profileId;
            if (currentUser.Equals(profileId))
            {
                return RedirectToAction("Error", "Response", new { message = "You cannot converse with yourself" });
            }
            var user = await _userService.GetUserById(profileId, currentUser);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Error", "Response", new { message = "Not Found User" });
        }
        [HttpGet]
        public async Task<ActionResult> GetMessageByByUserReceived()
        {
            var profileId = (Request.Headers.GetValues("ProfileId") ?? throw new InvalidOperationException()).FirstOrDefault();
            var currentRquest = (Request.Headers.GetValues("referer") ?? throw new InvalidOperationException()).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(profileId) && !profileId.ToLower().Equals("undefined"))
            {
                return await GetMessagesByUserReceivedAsync(profileId);
            }
            else if(!string.IsNullOrWhiteSpace(currentRquest))
            {
                Uri myUri = new Uri(currentRquest);
                profileId = HttpUtility.ParseQueryString(myUri.Query).Get("profileId");
                return await GetMessagesByUserReceivedAsync(profileId);
            }
            else
            {
                return PartialView("_Messages", new List<MessageViewModel>());
            }
        }

        private async Task<PartialViewResult> GetMessagesByUserReceivedAsync(string profileId)
        {
            var result = await _messageService.GetMessagesByUserReceivedAsync(currentUser, profileId);
            return PartialView("_Messages", result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMessage(MessageRequest message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new MessageViewModel()
                    {
                        UserSentId = currentUser,
                        UserReceivedId = message.UserReceivedId,
                        Content = message.Content,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Status = Status.Active
                    };
                    var res = await _messageService.CreateMessage(model);
                    if (res)
                    {
                        //Notify to all
                        SMSOnlineHub.BroadcastData();
                    }
                }
            }
            catch
            {

            }
            return RedirectToAction("Index","Chat", new { profileId = @message.UserReceivedId });
        }
    }
}
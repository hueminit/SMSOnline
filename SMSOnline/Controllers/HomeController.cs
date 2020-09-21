using Services;
using SMSOnline.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public HomeController(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        public async Task<ActionResult> Index()
        {
            if (IdentityHelper.CurrentUserLogged)
            {
               var res = await  _messageService.GetAllMessagesOfCurrentUser(IdentityHelper.CurrentUserId);
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
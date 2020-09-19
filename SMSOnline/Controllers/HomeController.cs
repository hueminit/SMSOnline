using System.Threading.Tasks;
using System.Web.Mvc;
using Services;
using SMSOnline.Helpers;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public async Task<ActionResult> Index()
        {
            if (IdentityHelper.CurrentUserLogged)
            {
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
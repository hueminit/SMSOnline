using System.Web.Mvc;

namespace SMSOnline.Controllers
{
    public class ResponseController : Controller
    {
        // GET: Error
        public ActionResult Error(string message)
        {
            ViewBag.Error = message;
            return View();
        }

        public ActionResult Success(string message)
        {
            ViewBag.Success = message;
            return View();
        }
    }
}
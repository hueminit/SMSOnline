using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SMSOnline.Controllers
{
    public class TestController : Controller
    {
        public IActionResult LoginTest()
        {
            return View();
        }

        public IActionResult RegisterTest()
        {
            return View();
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }
    }
}

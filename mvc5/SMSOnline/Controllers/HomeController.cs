﻿using System.Threading.Tasks;
using System.Web.Mvc;
using Services;

namespace SMSOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;
        public HomeController(ITestService testService)
        {
            _testService = testService;
        }
        public async Task<ActionResult> Index()
        {
            var res =  await  _testService.GetAllAsync();
            return View();
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
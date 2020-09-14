using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var res = _productService.GetAll();
            return View(res);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.Entities;
using Models.ViewModel;
using Services;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService; // Declare variable service class:_productService
        public ProductController(IProductService productService) // Init service class
        {
            _productService = productService;
        }
        public async Task<ActionResult> Index() // async : asynchronize function
        {
            var data = await _productService.GetAll();
            ViewBag.data = data.ToList();
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel product)
        {
            await _productService.AddAsync(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            ViewBag.product = await _productService.GetSingleById(id);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductViewModel product)
        {
            await _productService.UpdateAsync(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
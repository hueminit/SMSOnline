using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.ViewModel;
using Services;
using SMSOnline.Filters;

namespace SMSOnline.Controllers
{
    [Authorize]
    [CustomAuthorize(Users = "Admin")]
    public class SystemConfigController : Controller
    {
        private readonly ISystemConfigService _systemConfigService;
        public SystemConfigController(ISystemConfigService systemConfigService)
        {
            _systemConfigService = systemConfigService;
        }
        public async Task<ActionResult> Index()
        {
            var model = await _systemConfigService.GetAllConfigAsync();
            return View(model);
        }

        public async Task<ActionResult> Edit(string code)
        {
            var cofig = await _systemConfigService.GetConfigByCodeAsync(code);
            if(cofig != null)
            {
                return View(cofig);
            }
            return RedirectToAction("Error", "Response", new { message = "get config failure" });
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Code,ValueString,ValueNumber")] SystemConfigViewModel model)
        {
            var isUpdate = await _systemConfigService.CreateOrUpdateConfigAsync(model);
            if (isUpdate)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error", "Response", new { message = "update config failure" });
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.ViewModel;
using Models.ViewModel.Others;
using Services;
using SMSOnline.Helpers;

namespace SMSOnline.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositService _depositService;
        private readonly ICreditCardService _creditCardService;
        private readonly IUserService _userService;

        public DepositController(IDepositService depositService, ICreditCardService creditCardService)
        {
            _depositService = depositService;
            _creditCardService = creditCardService;
        }

        // GET: Deposit
        public async Task<ActionResult> Index()
        {

            DepositRequestModel depositRequest = new DepositRequestModel();
            var cardsFromDb = await _creditCardService.GetAllCreditCardsAsync(IdentityHelper.CurrentUserId);

            foreach (CreditCardViewModel creditCard in cardsFromDb)
            {
                depositRequest.CreditCardItems.Add(
                    new SelectListItem
                    {
                        Value = creditCard.Id.ToString(),
                        Text = $"Card Number: {creditCard.Number}"
                    }
                );
            }

            return View(depositRequest);

        }

        [HttpPost]
        public async Task<ActionResult> Index(DepositViewModel model)
        {
            var user = await _userService.GetUserById(IdentityHelper.CurrentUserId, IdentityHelper.CurrentUserId);
            if (user != null)
            {
                var res = await _depositService.CreateDepositAsync(model, user);
                if (res)
                {
                    return RedirectToAction("Success", "Response", new {message = "successful"});
                }
            }

            return RedirectToAction("Error", "Response", new {message = "Error when process "});
        }
    }
}
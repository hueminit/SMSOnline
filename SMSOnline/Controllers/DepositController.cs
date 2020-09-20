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
    [Authorize]
    public class DepositController : Controller
    {
        private readonly IDepositService _depositService;
        private readonly ICreditCardService _creditCardService;
        private readonly IUserService _userService;

        public DepositController(IDepositService depositService, ICreditCardService creditCardService, IUserService userService)
        {
            _depositService = depositService;
            _creditCardService = creditCardService;
            _userService = userService;
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
        public async Task<ActionResult> Index(DepositRequestModel model)
        {
            var user = await _userService.GetUserByIdAsync(IdentityHelper.CurrentUserId, IdentityHelper.CurrentUserId);
            if (user != null)
            {
                DepositViewModel deposit = new DepositViewModel();
                deposit.CreditCardId = model.Deposit.CreditCardId;
                deposit.Amount = model.Deposit.Amount;
                var res = await _depositService.CreateDepositAsync(deposit, user);
                if (res)
                {
                    return RedirectToAction("Success", "Response", new { message = "successful" });
                }
            }

            return RedirectToAction("Error", "Response", new {message = "Error when process "});
        }
    }
}
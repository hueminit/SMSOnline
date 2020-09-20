using Models.ViewModel.Others;
using Services;
using SMSOnline.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        public async Task<ActionResult> Index()
        {
            string customerId = IdentityHelper.CurrentUserId;
            var creditCards = await _creditCardService.GetAllCreditCardsAsync(customerId);
            return View(creditCards);
        }

        public ActionResult Create()
        {
            CreditCardRequestModel creditCard = new CreditCardRequestModel();
            return View(creditCard);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreditCardRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _creditCardService.Create(model, IdentityHelper.CurrentUserId);
            if (res)
            {
                return RedirectToAction("Index", "CreditCard");
            }
            return View(model);
        }
    }
}
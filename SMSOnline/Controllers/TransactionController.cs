using Services;
using SMSOnline.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMSOnline.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: Transaction
        public async Task<ActionResult> Index()
        {
            var res = await _transactionService.GetAllTransactionsById(IdentityHelper.CurrentUserId);
            return View(res);
        }
    }
}
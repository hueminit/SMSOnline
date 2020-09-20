using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Services;
using SMSOnline.Helpers;

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
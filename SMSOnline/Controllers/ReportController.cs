using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Services;

namespace SMSOnline.Controllers
{
    public class ReportController : Controller
    {
        private readonly ITransactionService _transactionService;
        public ReportController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        // GET: Report
        public async Task<ActionResult> Index(int page = 1)
        {
            var transactions = await _transactionService.GetAllTransactions(page);
            return View(transactions);
        }
    }
}
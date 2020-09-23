using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.Shared;
using Models.ViewModel;
using Models.ViewModel.Others;
using Services;
using SMSOnline.Filters;

namespace SMSOnline.Controllers
{
    [Authorize]
    [CustomAuthorize(Users = "Admin")]
    public class ReportController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        public ReportController(ITransactionService transactionService, IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }
        // GET: Report
        public async Task<ActionResult> Index(string keyword,int page = 1)
        {
            var transactions = await _transactionService.GetAllDeposits(keyword,page);
            transactions.Transaction.keyword = keyword;
            return View(transactions);
        }

        public async Task<ActionResult> TransationUser(string keyword, int page = 1)
        {
            ViewBag.Keyword = keyword;
            var check = new CheckAccountViewModel()
            {
                Email = keyword,
                PhoneNumber = keyword,
                UserName = keyword
            };
            var user = await _userService.GetUserByConditionAsync(check);
            if (user != null)
            {
                var transactions = await _transactionService.GetAllTransactionsById(user.Id, page);
                transactions.keyword = keyword;
                return View(transactions);
            }

            var data = new PaginationSet<TransactionViewModel>()
            {
                Page = page,
                TotalCount = 1,
                TotalPages = 1,
                Items = new List<TransactionViewModel>(),
                MaxPage = 10
            };
            return View(data);
        }
        
    }
}
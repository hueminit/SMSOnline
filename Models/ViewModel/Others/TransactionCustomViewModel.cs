using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Shared;

namespace Models.ViewModel.Others
{
    public class TransactionCustomViewModel
    {
        public TransactionCustomViewModel()
        {
            Transaction = new PaginationSet<TransactionViewModel>();

        }

        public PaginationSet<TransactionViewModel> Transaction { set; get; }
        public decimal TotalAmount { set; get; } = 0;
    }
}

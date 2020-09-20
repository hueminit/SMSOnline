using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Models.Entities;

namespace Models.ViewModel.Others
{
    public class DepositRequestModel
    {
        public DepositRequestModel()
        {
            CreditCardItems = new List<SelectListItem>();
            Deposit = new DepositViewModel();
        }
        public DepositViewModel Deposit { set; get; }
        public List<SelectListItem> CreditCardItems  { set; get; }
    }
}

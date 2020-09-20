using System.Collections.Generic;
using System.Web.Mvc;

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
        public List<SelectListItem> CreditCardItems { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel.Others
{
    public class MessageCustomViewModel
    {
        public string UserReceivedId { set; get; }
        public string UserReceivedName { set; get; }
        public string Message { set; get; }
        public DateTime DateCreated { set; get; }
    }
}

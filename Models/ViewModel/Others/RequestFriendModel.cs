using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel.Others
{
    public class RequestFriendModel
    {
       public bool IsCurrentUserSendRequest { set; get; }
       public bool IsFriend { set; get; }
       public bool StatustRequest { set; get; }
    }
}

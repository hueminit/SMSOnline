using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using Models.Shared;

namespace Models.ViewModel.Others
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            UserReceived = new AppUserViewModel();
            //ListChat = new PaginationSet<MessageViewModel>();
            Chat = new ContactChatViewModel();

        }
        public AppUserViewModel UserReceived { set; get; }
        public ContactChatViewModel Chat { set; get; }
        //public PaginationSet<MessageViewModel> ListChat { set; get; }

    }

    public class ContactChatViewModel
    {
        public ContactChatViewModel()
        {
            ListChat = new List<MessageViewModel>();
            Contact = new List<ContactViewModel>();
        }
        public List<MessageViewModel> ListChat { set; get; }
        public List<ContactViewModel> Contact { set; get; }
    }
}

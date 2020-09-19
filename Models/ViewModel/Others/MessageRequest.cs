using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel.Others
{
    public class MessageRequest
    {
        [Required]
        public string UserReceivedId { set; get; }

        [Required]
        public string Content { set; get; }
    }
}

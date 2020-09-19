using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;

namespace Models.ViewModel
{
   public class MessageViewModel
    {
        public int Id { set; get; }

        [Required]
        public string UserSentId { set; get; }

        [Required]
        public string UserReceivedId { set; get; }

        public DateTime DateCreated { get; set; }
        public string DateCreatedFormat => DateCreated.ToString("hh:mm tt");
        public bool IsToDay => (DateCreated.Date == DateTime.Today.Date);
        public DateTime DateModified { get; set; }

        [Required]
        public string Content { get; set; }
        public Status Status { get; set; }

        public bool IsCurrentUserSent { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ContactViewModel
    {
        public int Id { set; get; }

        [Required]
        [StringLength(255)]
        public string FullName { set; get; }

        [Required]
        public string ContactSentId { set; get; }

        [Required]
        public string ContactReceivedId { set; get; }

        public bool IsFriend { set; get; }

        public bool IsBlock { set; get; }

        public bool StatusRequest { set; get; }

        [Required]
        public string PhoneNumber { set; get; }
    }
}

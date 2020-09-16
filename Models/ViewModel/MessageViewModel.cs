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
        public string UserId { set; get; }

        [Required]
        public string ContactId { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [Required]
        public string Content { get; set; }
        public Status Status { get; set; }
    }
}

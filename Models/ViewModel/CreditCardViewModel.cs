using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    class CreditCardViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

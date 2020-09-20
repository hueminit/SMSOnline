using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel.Others
{
    public class CreditCardRequestModel
    {
        [Required]
        [StringLength(16, MinimumLength = 12)]
        [Display(Name = "Card Number")]
        public string Number { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 3)]
        [Display(Name = "Card CVV")]
        public string CVV { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}

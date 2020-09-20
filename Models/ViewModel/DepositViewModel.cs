using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class DepositViewModel
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CreditCardId { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

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
        [Required]
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

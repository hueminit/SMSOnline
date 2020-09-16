using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;

namespace Models.ViewModel
{
    public class TransactionViewModel
    {
        public int Id { set; get; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public decimal Price { get; set; }

        public TransactionType Type { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

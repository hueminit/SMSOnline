using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

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
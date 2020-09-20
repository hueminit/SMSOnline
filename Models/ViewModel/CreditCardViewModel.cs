using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class CreditCardViewModel
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
﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class DepositViewModel
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [DisplayName("Credit Card")]
        public string CreditCardId { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
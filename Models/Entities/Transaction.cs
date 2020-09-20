using Models.Enums;
using Models.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("Transactions")]
    public class Transaction : DomainEntity<int>
    {
        public DateTime CreatedAt { get; set; }

        public decimal Price { get; set; }

        public TransactionType Type { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
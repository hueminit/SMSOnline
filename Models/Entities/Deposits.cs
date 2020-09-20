using Models.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("Deposits")]
    public class Deposit : DomainEntity<int>
    {
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
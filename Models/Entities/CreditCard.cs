using Models.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("CreditCards")]
    public class CreditCard : DomainEntity<int>
    {
        public string Number { get; set; }

        public string CVV { get; set; }

        public DateTime DateRegistered { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
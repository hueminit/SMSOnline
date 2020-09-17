using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Shared;

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;
using Models.Shared;

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Shared;

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

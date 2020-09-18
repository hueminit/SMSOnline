using Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    [Table("Products")]
    public class Product : DomainEntity<int>
    {
        [StringLength(255)]
        public string ProductName { set; get; }

        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
    }
}

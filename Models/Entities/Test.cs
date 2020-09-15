using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Shared;
using Models.Shared;

namespace Models.Entities
{
    [Table("Tests")]
    public class Test : DomainEntity<int>
    {
        [StringLength(255)]
        public string Name { set; get; }
    }
}

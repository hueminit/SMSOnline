using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Shared;

namespace Models.Entities
{
    [Table("SystemConfigs")]
    public class SystemConfig : DomainEntity<int>
    {
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Code { set; get; }
        [MaxLength(50)]
        public string ValueString { set; get; }
        public decimal? ValueNumber { set; get; }
    }
}

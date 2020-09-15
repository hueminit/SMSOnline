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
        public Test()
        {
        }

        public Test(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Test(string name)
        {
            Name = name;
        }

        [StringLength(255)]
        public string Name { set; get; }
    }
}

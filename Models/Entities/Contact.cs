using Model.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Shared;

namespace Models.Entities
{
    [Table("Contacts")]
    public class Contact : DomainEntity<int>
    {
        [StringLength(255)]
        public string FullName { set; get; }

        public string ContactSentId { set; get; }
        //public virtual AppUser ContactSentRequest { set; get; }

        public string ContactReceivedId { set; get; }
        public virtual AppUser ContactReceivedRequest { set; get; }

        public bool IsFriend { set; get; }

        public bool IsBlock { set; get; }

        public bool StatusRequest { set; get; }

        public string PhoneNumber { set; get; }
    }
}
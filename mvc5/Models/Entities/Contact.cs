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
        public Contact()
        {
        }

        public Contact(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public Contact(string fullName)
        {
            FullName = fullName;
        }

        [StringLength(255)]
        public string FullName { set; get; }

        public string UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual AppUser User { set; get; }

        public bool IsFriend { set; get; }

        public bool StatusRequest { set; get; }

        public string PhoneNumber { set; get; }
    }
}
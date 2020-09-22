using Models.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("Contacts")]
    public class Contact : DomainEntity<int>
    {
        [StringLength(255)]
        public string FullNameContactSent { set; get; }

        [StringLength(255)]
        public string FullNameContactReceived { set; get; }

        public string ContactSentId { set; get; }

        public string ContactReceivedId { set; get; }
        public virtual AppUser ContactReceivedRequest { set; get; }

        public bool IsFriend { set; get; }

        public bool StatusRequest { set; get; }

        public string PhoneNumber { set; get; }
    }
}
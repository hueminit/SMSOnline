using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class ContactViewModel
    {
        public int Id { set; get; }

        [Required]
        [StringLength(255)]
        public string FullNameContactSent { set; get; }

        [StringLength(255)]
        [Required]
        public string FullNameContactReceived { set; get; }

        [Required]
        public string ContactSentId { set; get; }

        [Required]
        public string ContactReceivedId { set; get; }

        public bool IsFriend { set; get; }

        public bool IsBlock { set; get; }

        public bool StatusRequest { set; get; }

        [Required]
        public string PhoneNumber { set; get; }
    }
}
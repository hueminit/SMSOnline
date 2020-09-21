using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel.Others
{
    public class MessageRequest
    {
        [Required]
        public string UserReceivedId { set; get; }

        [Required]
        public string Content { set; get; }

        public string FullNameReceived { set; get; }
    }
}
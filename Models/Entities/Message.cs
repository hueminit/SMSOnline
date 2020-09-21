using Models.Enums;
using Models.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("Messages")]
    public class Message : DomainEntity<int>, IDateTracking
    {
        [StringLength(255)]
        public string FullNameSent { set; get; }

        [StringLength(255)]
        public string FullNameReceived { set; get; }

        public string UserSentId { set; get; }

        public string UserReceivedId { set; get; }
        public AppUser UserReceived { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [StringLength(120)]
        public string Content { get; set; }
        public Status Status { get; set; }
    }
}
using Models.Enums;
using Models.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("Messages")]
    public class Message : DomainEntity<int>, IDateTracking
    {
        public string UserSentId { set; get; }
        //public AppUser UserSent { set; get; }

        public string UserReceivedId { set; get; }
        public AppUser UserReceived { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string Content { get; set; }
        public Status Status { get; set; }
    }
}
﻿using Models.Enums;
using Model.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Shared;

namespace Models.Entities
{
    [Table("Messages")]
    public class Message : DomainEntity<int>, IDateTracking
    {
        public string UserId { set; get; }
        [ForeignKey("UserId")]
        public virtual AppUser Sender { set; get; }

        public string ContactId { set; get; }
        [ForeignKey("ContactId")]
        public virtual AppUser Receiver { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string Content { get; set; }
        public Status Status { get; set; }
    }
}
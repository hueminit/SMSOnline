﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Model.Enums;
using Model.Shared;

namespace Model.Entites
{

    [Table("Messages")]
    public class Message : DomainEntity<int>, IDateTracking
    {
        public Message()
        {
        }
        public Message(int id,Guid userId, Guid contactId,
            DateTime dateCreated,DateTime dateModified,string content,Status status)
        {
            Id = id;
            UserId = userId;
            ContactId = contactId;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Content = content;
            Status = status;

        }

        public Message(Guid userId, Guid contactId,
            DateTime dateCreated, DateTime dateModified, string content, Status status)
        {
            UserId = userId;
            ContactId = contactId;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Content = content;
            Status = status;

        }


        public Guid UserId { set; get; }
        public virtual AppUser Sender { set; get; }

        public Guid ContactId { set; get; }
        public virtual AppUser Receiver { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string Content { get; set; }
        public Status Status { get; set; }
    }
}
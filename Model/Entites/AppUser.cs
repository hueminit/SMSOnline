using Microsoft.AspNetCore.Identity;
using Model.Enums;
using Model.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Model.Entites
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable,IsDelete
    {
        public AppUser()
        {
        }

        public AppUser(Guid id, string fullName, string userName, string email, string phoneNumber, string avatar, Status status,
            string address, Gender gender,bool isDelete, DateTime? birthDay)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            Status = status;
            Address = address;
            Gender = gender;
            IsDelete = isDelete;
            BirthDay = birthDay;
        }

        public string FullName { get; set; }
        public DateTime? BirthDay { set; get; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [DefaultValue(Status.Active)]
        public Status Status { set; get; } = Status.Active;

        public bool IsDelete { get; set; }

        //public virtual ICollection<Message> MessagesSent { get; set; }
        //public virtual ICollection<Message> MessagesReceived { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
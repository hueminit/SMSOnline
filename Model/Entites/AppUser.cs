using Microsoft.AspNetCore.Identity;
using Model.Enums;
using Model.Shared;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entites
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public AppUser()
        {
        }

        public AppUser(Guid id, string fullName, string userName, string email, string phoneNumber, string avatar, Status status,
            string address, Gender gender)
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
        }

        public string FullName { get; set; }
        public DateTime? BirthDay { set; get; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [DefaultValue(Status.Active)]
        public Status Status { set; get; } = Status.Active;
    }
}
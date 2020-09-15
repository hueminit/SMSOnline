using Microsoft.AspNet.Identity.EntityFramework;
using Models.Enums;
using Model.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Models.Shared;

namespace Models.Entities
{
    [Table("AppUsers")]
    public sealed class AppUser : IdentityUser, IDateTracking, ISwitchable, IsDelete
    {
        public string FullName { get; set; }
        public decimal Balance { get; set; }
        public DateTime? BirthDay { set; get; }
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

        public ICollection<Contact> Contacts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager,
            string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
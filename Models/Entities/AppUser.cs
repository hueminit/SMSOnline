using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Enums;
using Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Models.Entities
{
    [Table("AppUsers")]
    public sealed class AppUser : IdentityUser, IDateTracking, ISwitchable
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

        public int TotalFreeMessage { get; set; }

        [StringLength(256)]
        public string Description { set; get; }

        [InverseProperty("UserReceived")]
        public ICollection<Message> MessagesReceived { get; set; }

        [InverseProperty("ContactReceivedRequest")]
        public ICollection<Contact> ContactReceived { get; set; }

        public ICollection<Deposit> Deposits { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

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
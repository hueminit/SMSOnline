using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<string>();
        }

        public string Id { set; get; }

        [StringLength(70, MinimumLength = 3)]
        [Required]
        public string FullName { set; get; }

        [DataType(DataType.Date)]
        public DateTime? BirthDay { set; get; }

        [Required]
        public string Email { set; get; }

        public string Password { set; get; }

        [Required]
        public string UserName { set; get; }

        public decimal Balance { get; set; }

        public string Address { get; set; }

        [StringLength(15, MinimumLength = 9)]
        public string PhoneNumber { set; get; }

        public string Avatar { get; set; }

        public Status Status { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public List<string> Roles { get; set; }

        public bool IsFriendWithCurrentUser { get; set; }
        public bool IsCurrentUserSendRequest { get; set; }
        public bool StatustRequest { get; set; }
        public int TotalFreeMessage { get; set; }
        
        [StringLength(256)]
        public string Description { set; get; }

    }
}
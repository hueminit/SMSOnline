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

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { set; get; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { set; get; }

        [Required]
        public string UserName { set; get; }

        public decimal Balance { get; set; }

        public string Address { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Entered phone format is not valid.")]
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
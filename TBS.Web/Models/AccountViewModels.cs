using System;
using System.ComponentModel.DataAnnotations;

namespace TBS.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public int LoginTime { get; set; }
    }

    public class CreateUserViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Secret Question")]
        public string SecretQuestion { get; set; }

        [Display(Name = "Secret Answer")]
        public string SecretAnswer { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Display(Name = "Phone 1")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [Display(Name = "Phone 2")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        [Display(Name = "Address 1")]
        public string Address1 { get; set; }


        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        public bool Active { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string Address { get { return string.IsNullOrEmpty(Address1) ? Address2 : Address1; } }

    }


    public class UserListView
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Secret Question")]
        public string SecretQuestion { get; set; }

        [Display(Name = "Secret Answer")]
        public string SecretAnswer { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Display(Name = "Phone 1")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [Display(Name = "Phone 2")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        [Display(Name = "Address 1")]
        public object Address1 { get; set; }


        [Display(Name = "Address 2")]
        public object Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public class UserInRole
    {
        public string UserId { get; set; }
        public bool IsInRole { get; set; }
        public string RoleId { get; set; }

        public string Name { get; set; }
    }

}

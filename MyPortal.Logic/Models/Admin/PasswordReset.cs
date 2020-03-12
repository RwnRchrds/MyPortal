using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Authorisation.Attributes;

namespace MyPortal.Logic.Models.Admin
{
    public class PasswordReset
    {
        public Guid UserId { get; set; }
        
        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
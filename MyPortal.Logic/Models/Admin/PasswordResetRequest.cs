using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Admin
{
    public class PasswordResetRequest
    {
        public Guid UserId { get; set; }
        
        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
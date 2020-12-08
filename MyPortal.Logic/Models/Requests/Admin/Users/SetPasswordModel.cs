using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Admin.Users
{
    public class SetPasswordModel
    {
        public Guid UserId { get; set; }
        
        [Required]
        public string NewPassword { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Admin.Users
{
    public class SetPasswordRequestModel
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
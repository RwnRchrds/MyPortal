using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
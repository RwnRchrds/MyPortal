using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MyPortal.Database.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}

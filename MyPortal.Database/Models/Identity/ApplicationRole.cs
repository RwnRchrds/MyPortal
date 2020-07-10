﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>, IEntity
    {
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool System { get; set; }

        public virtual ICollection<ApplicationRolePermission> RolePermissions { get; set; }
    }
}

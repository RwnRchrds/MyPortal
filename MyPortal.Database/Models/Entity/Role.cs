using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Roles")]
    public class Role : IdentityRole<Guid>, ISystemEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RoleClaims = new HashSet<RoleClaim>();
        }
        
        public string Description { get; set; }
        public byte[] Permissions { get; set; }
        public bool System { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}

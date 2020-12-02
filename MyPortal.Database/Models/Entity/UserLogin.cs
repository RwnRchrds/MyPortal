using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyPortal.Database.Models.Entity
{
    [Table("UserLogins")]
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MyPortal.Database.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual Person Person { get; set; }

        public string GetDisplayName()
        {
            return Person != null ? Person.FirstName : UserName;
        }
    }
}

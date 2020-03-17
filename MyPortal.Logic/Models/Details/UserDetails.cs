using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Logic.Models.Details
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUsername { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Username { get; set; }
        public string UserType { get; set; }

        public virtual PersonDetails Person { get; set; }

        public string GetDisplayName(bool salutationFormat = false)
        {
            if (Person != null)
            {
                return Person.GetDisplayName(salutationFormat);
            }

            return Username;
        }
    }
}

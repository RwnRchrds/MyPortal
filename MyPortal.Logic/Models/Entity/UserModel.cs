using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class UserModel : BaseModel
    {
        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public DateTimeOffset LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public Guid? PersonId { get; set; }
        
        public int UserType { get; set; }
        
        public bool Enabled { get; set; }

        public virtual PersonModel Person { get; set; }

        public string GetDisplayName(bool salutationFormat = false)
        {
            return Person != null ? Person.GetDisplayName(salutationFormat) : UserName;
        }
    }
}

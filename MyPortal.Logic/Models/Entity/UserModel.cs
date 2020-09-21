using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class UserModel
    {
        public Guid Id { get; set; }
        
        [StringLength(256)]
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? PersonId { get; set; }

        [StringLength(1)]
        public int UserType { get; set; }

        public bool Enabled { get; set; }

        public virtual PersonModel Person { get; set; }

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

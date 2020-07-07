using System;

namespace MyPortal.Logic.Models.Entity
{
    public class UserModel
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
        public string UserName { get; set; }
        public string UserType { get; set; }
        public Guid? SelectedAcademicYearId { get; set; }
        public bool Enabled { get; set; }

        public virtual AcademicYearModel SelectedAcademicYear { get; set; }
        public virtual PersonModel Person { get; set; }

        public string GetDisplayName(bool salutationFormat = false)
        {
            if (Person != null)
            {
                return Person.GetDisplayName(salutationFormat);
            }

            return UserName;
        }
    }
}

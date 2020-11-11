using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class PersonModel : BaseModel
    {
        public Guid DirectoryId { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(256)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string ChosenFirstName { get; set; }

        public Guid? PhotoId { get; set; }

        [StringLength(10)]
        public string NhsNumber { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime? Deceased { get; set; }

        public Guid? EthnicityId { get; set; }

        public bool Deleted { get; set; }

        //public virtual ContactModel ContactDetails { get; set; }

        //public virtual StaffMemberModel StaffMemberDetails { get; set; }

        //public virtual StudentModel StudentDetails { get; set; }

        //public virtual AgentModel AgentDetails { get; set; }

        //public virtual UserModel User { get; set; }

        public virtual DirectoryModel Directory { get; set; }

        public virtual PhotoModel Photo { get; set; }

        public virtual EthnicityModel Ethnicity { get; set; }

        public string GetDisplayName(bool salutationFormat = false)
        {
            return salutationFormat ? $"{Title} {FirstName.Substring(0, 1)} {LastName}" : $"{LastName}, {FirstName}";
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Logic.Models.Business
{
    public class PersonModel
    {
        public Guid Id { get; set; }

        public Guid DirectoryId { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(256)]
        public string MiddleName { get; set; }

        public int? PhotoId { get; set; }

        [StringLength(256)]
        public string NhsNumber { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Deceased { get; set; }

        public Guid? UserId { get; set; }

        public bool Deleted { get; set; }

        public string GetDisplayName(bool salutationFormat = false)
        {
            return salutationFormat ? $"{Title} {FirstName.Substring(0, 1)} {LastName}" : $"{LastName}, {FirstName}";
        }
    }
}
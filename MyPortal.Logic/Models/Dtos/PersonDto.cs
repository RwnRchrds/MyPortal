using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Logic.Models.Dtos
{
    public class PersonDto
    {
        public Guid Id { get; set; }

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

        public DateTime? Dob { get; set; }

        public DateTime? Deceased { get; set; }

        public string UserId { get; set; }

        public bool Deleted { get; set; }

        public virtual ContactDto ContactDetails { get; set; }

        public virtual StaffMemberDto StaffMemberDetails { get; set; }

        public virtual StudentDto StudentDetails { get; set; }

        public string GetDisplayName()
        {
            return $"{LastName}, {FirstName}";
        }
    }
}

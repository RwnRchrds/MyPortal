using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("People")]
    public class Person : BaseTypes.Entity, IDirectoryEntity, ISoftDeleteEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            PhoneNumbers = new HashSet<PhoneNumber>();
            MedicalConditions = new HashSet<PersonCondition>();
            DietaryRequirements = new HashSet<PersonDietaryRequirement>();
            HeadteacherOf = new HashSet<School>();
            EmailAddresses = new HashSet<EmailAddress>();
            AddressPeople = new HashSet<AddressPerson>();
            DiaryEventInvitations = new HashSet<DiaryEventAttendee>();
            AssignedTasks = new HashSet<Task>();
        }

        [Column(Order = 2)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 3)]
        [StringLength(128)]
        public string Title { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string PreferredFirstName { get; set; }
        
        [Column(Order = 5)]
        [StringLength(256)]
        public string PreferredLastName { get; set; }

        [Column(Order = 6)]
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Column(Order = 7)]
        [StringLength(256)] 
        public string MiddleName { get; set; }

        [Column(Order = 8)]
        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Column(Order = 9)]
        public Guid? PhotoId { get; set; }

        [Column(Order = 10)]
        [StringLength(10)]
        public string NhsNumber { get; set; }

        [Column(Order = 11)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 12)]
        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Column(Order = 13, TypeName = "date")]
        public DateTime? Dob { get; set; }

        [Column(Order = 14, TypeName = "date")] 
        public DateTime? Deceased { get; set; }

        [Column(Order = 15)]
        public Guid? EthnicityId { get; set; }

        [Column(Order = 16)]
        public bool Deleted { get; set; }
        
        public virtual Photo Photo { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        public virtual Directory Directory { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<StaffMember> StaffMembers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<PersonCondition> MedicalConditions { get; set; }
        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }
        public virtual ICollection<PersonDietaryRequirement> DietaryRequirements { get; set; }
        public virtual ICollection<School> HeadteacherOf { get; set; }
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
        public virtual ICollection<AddressPerson> AddressPeople { get; set; }
        public virtual ICollection<DiaryEventAttendee> DiaryEventInvitations { get; set; }
        public virtual ICollection<Task> AssignedTasks { get; set; }
        public virtual ICollection<NextOfKin> RelatedStaff { get; set; }
    }
}
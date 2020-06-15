using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    public static class Gender
    {
        public const string Male = "M";
        public const string Female = "F";
        public const string Other = "X";
        public const string Unknown = "U";
    }

    [Table("Person")]
    public class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            PhoneNumbers = new HashSet<PhoneNumber>();
            MedicalConditions = new HashSet<PersonCondition>();
            DietaryRequirements = new HashSet<PersonDietaryRequirement>();
            HeadteacherOf = new HashSet<School>();
            EmailAddresses = new HashSet<EmailAddress>();
            Addresses = new HashSet<AddressPerson>();
            DiaryEventInvitations = new HashSet<DiaryEventAttendee>();
            AssignedTo = new HashSet<Task>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid DirectoryId { get; set; }

        [DataMember]
        [StringLength(128)]
        public string Title { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(256)] 
        public string MiddleName { get; set; }

        [DataMember]
        public int? PhotoId { get; set; }

        [DataMember]
        [StringLength(256)]
        public string NhsNumber { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [DataMember]
        [Column(TypeName = "date")] 
        public DateTime? Deceased { get; set; }

        [DataMember]
        public Guid? UserId { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        public virtual Contact ContactDetails { get; set; }

        public virtual Directory Directory { get; set; }

        public virtual StaffMember StaffMemberDetails { get; set; }

        public virtual Student StudentDetails { get; set; }

        public virtual ApplicationUser User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCondition> MedicalConditions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonDietaryRequirement> DietaryRequirements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<School> HeadteacherOf { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressPerson> Addresses { get; set; }

        public virtual ICollection<DiaryEventAttendee> DiaryEventInvitations { get; set; }

        public virtual ICollection<Task> AssignedTo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Subject")]
    public partial class Subject : ISoftDelete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            Classes = new HashSet<Class>();
            StudyTopics = new HashSet<StudyTopic>();
            GiftedTalentedStudents = new HashSet<GiftedTalented>();
            StaffMembers = new HashSet<SubjectStaffMember>();
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Column(Order = 3)]
        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Classes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudyTopic> StudyTopics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiftedTalented> GiftedTalentedStudents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectStaffMember> StaffMembers { get; set; } 
    }
}

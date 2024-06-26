using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Subjects")]
    public class Subject : BaseTypes.Entity, ISoftDeleteEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            Courses = new HashSet<Course>();
            GiftedTalentedStudents = new HashSet<GiftedTalented>();
            StaffMembers = new HashSet<SubjectStaffMember>();
        }

        [Column(Order = 2)] public Guid SubjectCodeId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        [Column(Order = 5)] public bool Deleted { get; set; }

        public virtual SubjectCode SubjectCode { get; set; }


        public virtual ICollection<Course> Courses { get; set; }


        public virtual ICollection<GiftedTalented> GiftedTalentedStudents { get; set; }


        public virtual ICollection<SubjectStaffMember> StaffMembers { get; set; }

        public virtual ICollection<CurriculumGroupSession> CurriculumGroupSessions { get; set; }
    }
}
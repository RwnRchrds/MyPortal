using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Achievement")]
    public class Achievement
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public Guid AchievementTypeId { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid LocationId { get; set; }

        [DataMember]
        public Guid RecordedById { get; set; }

        [DataMember]
        public Guid? OutcomeId { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public int Points { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        public virtual AchievementType Type { get; set; }

        public virtual Location Location { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual ApplicationUser RecordedBy { get; set; }

        public virtual Student Student { get; set; }

        public virtual AchievementOutcome Outcome { get; set; }
    }
}
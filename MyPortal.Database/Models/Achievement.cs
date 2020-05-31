using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Achievement")]
    public class Achievement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid AchievementTypeId { get; set; }

        public Guid StudentId { get; set; }

        public Guid LocationId { get; set; }

        public Guid RecordedById { get; set; }

        public Guid? OutcomeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Deleted { get; set; }

        public virtual AchievementType Type { get; set; }

        public virtual Location Location { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual ApplicationUser RecordedBy { get; set; }

        public virtual Student Student { get; set; }

        public virtual AchievementOutcome Outcome { get; set; }
    }
}
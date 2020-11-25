using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Achievements")]
    public class Achievement : BaseTypes.Entity, ISoftDeleteEntity
    {
        [Column(Order = 1)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 2)]
        public Guid AchievementTypeId { get; set; }

        [Column(Order = 3)]
        public Guid StudentId { get; set; }

        [Column(Order = 4)]
        public Guid LocationId { get; set; }

        [Column(Order = 5)]
        public Guid RecordedById { get; set; }

        [Column(Order = 6)]
        public Guid? OutcomeId { get; set; }

        [Column(Order = 7, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 8)]
        public string Comments { get; set; }

        [Column(Order = 9)]
        public int Points { get; set; }

        [Column(Order = 10)]
        public bool Deleted { get; set; }

        public virtual AchievementType Type { get; set; }

        public virtual Location Location { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual User RecordedBy { get; set; }

        public virtual Student Student { get; set; }

        public virtual AchievementOutcome Outcome { get; set; }
    }
}
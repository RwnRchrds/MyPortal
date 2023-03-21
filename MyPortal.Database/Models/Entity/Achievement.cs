using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Achievements")]
    public class Achievement : BaseTypes.Entity, ISoftDeleteEntity, ICreatable
    {
        [Column(Order = 2)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 3)]
        public Guid AchievementTypeId { get; set; }

        [Column(Order = 4)]
        public Guid? LocationId { get; set; }

        [Column(Order = 5)]
        public Guid CreatedById { get; set; }

        [Column(Order = 6)] 
        public DateTime CreatedDate { get; set; }

        [Column(Order = 7, TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(Order = 8)]
        public string Comments { get; set; }

        [Column(Order = 9)]
        public bool Deleted { get; set; }

        public virtual AchievementType Type { get; set; }

        public virtual Location Location { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual ICollection<StudentAchievement> InvolvedStudents { get; set; }
    }
}
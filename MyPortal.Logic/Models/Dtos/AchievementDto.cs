using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Dtos
{
    public class AchievementDto
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid AchievementTypeId { get; set; }

        public Guid StudentId { get; set; }

        public Guid LocationId { get; set; }

        public Guid RecordedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        [NotNegative]
        public int Points { get; set; }

        public bool Deleted { get; set; }

        public virtual AchievementTypeDto Type { get; set; }

        public virtual LocationDto Location { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual ApplicationUser RecordedBy { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}

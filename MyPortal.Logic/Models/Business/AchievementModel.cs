using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Business
{
    public class AchievementModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid AchievementTypeId { get; set; }

        public Guid StudentId { get; set; }

        public Guid LocationId { get; set; }

        public Guid RecordedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Deleted { get; set; }

        public virtual AchievementTypeModel Type { get; set; }

        public virtual LocationModel Location { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual UserModel RecordedBy { get; set; }

        public virtual StudentModel Student { get; set; }
    }
}

using System;
using MyPortal.Database.Models;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Models.ListModels;

namespace MyPortal.Logic.Models.Entity
{
    public class AchievementModel
    {
        public Guid Id { get; set; }

        [NotEmpty]
        public Guid AcademicYearId { get; set; }

        [NotEmpty]
        public Guid AchievementTypeId { get; set; }

        [NotEmpty]
        public Guid StudentId { get; set; }

        [NotEmpty]
        public Guid LocationId { get; set; }

        
        public Guid RecordedById { get; set; }

        [NotEmpty]
        public Guid OutcomeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Deleted { get; set; }

        public virtual AchievementTypeModel Type { get; set; }

        public virtual AchievementOutcomeModel Outcome { get; set; }

        public virtual LocationModel Location { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual UserModel RecordedBy { get; set; }

        public virtual StudentModel Student { get; set; }

        public AchievementListModel ToListModel()
        {
            return new AchievementListModel(this);
        }
    }
}

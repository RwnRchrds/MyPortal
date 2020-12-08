using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Requests.Behaviour;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;

namespace MyPortal.Logic.Models.Entity
{
    public class AchievementModel : BaseModel
    {
        public AchievementModel()
        {

        }

        public AchievementModel(CreateAchievementModel model, Guid userId)
        {
            if (model is UpdateAchievementModel updateModel)
            {
                Id = updateModel.Id;
            }

            AcademicYearId = model.AcademicYearId;
            AchievementTypeId = model.AchievementTypeId;
            StudentId = model.StudentId;
            LocationId = model.LocationId;
            RecordedById = userId;
            OutcomeId = model.OutcomeId;
            CreatedDate = DateTime.Now;
            Comments = model.Comments;
            Points = model.Points;
        }

        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "Achievement Type is required.")]
        public Guid AchievementTypeId { get; set; }

        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public Guid LocationId { get; set; }
        
        public Guid RecordedById { get; set; }

        public Guid? OutcomeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        [Required(ErrorMessage = "Points is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Points cannot be negative.")]
        public int Points { get; set; }

        public bool Deleted { get; set; }

        public virtual AchievementTypeModel Type { get; set; }

        public virtual AchievementOutcomeModel Outcome { get; set; }

        public virtual LocationModel Location { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual UserModel RecordedBy { get; set; }

        public virtual StudentModel Student { get; set; }

        public AchievementDataGridModel ToListModel()
        {
            return new AchievementDataGridModel(this);
        }
    }
}

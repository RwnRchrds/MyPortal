using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.List;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AchievementModel : BaseModel, ILoadable
    {
        public AchievementModel(Achievement model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Achievement model)
        {
            AcademicYearId = model.AcademicYearId;
            AchievementTypeId = model.AchievementTypeId;
            StudentId = model.StudentId;
            LocationId = model.LocationId;
            CreatedById = model.CreatedById;
            OutcomeId = model.OutcomeId;
            CreatedDate = model.CreatedDate;
            Comments = model.Comments;
            Points = model.Points;
            Deleted = model.Deleted;

            if (model.Type != null)
            {
                Type = new AchievementTypeModel(model.Type);
            }

            if (model.Outcome != null)
            {
                Outcome = new AchievementOutcomeModel(model.Outcome);
            }

            if (model.Location != null)
            {
                Location = new LocationModel(model.Location);
            }

            if (model.AcademicYear != null)
            {
                AcademicYear = new AcademicYearModel(model.AcademicYear);
            }

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }
        }

        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "Achievement Type is required.")]
        public Guid AchievementTypeId { get; set; }

        public Guid StudentId { get; set; }

        public Guid? LocationId { get; set; }
        
        public Guid CreatedById { get; set; }

        public Guid? OutcomeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        [Required(ErrorMessage = "Points is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Points cannot be negative.")]
        public int Points { get; set; }

        public bool Deleted { get; set; }

        public AchievementTypeModel Type { get; set; }

        public AchievementOutcomeModel Outcome { get; set; }

        public LocationModel Location { get; set; }

        public AcademicYearModel AcademicYear { get; set; }

        public UserModel CreatedBy { get; set; }

        public StudentModel Student { get; set; }

        public AchievementDataGridModel ToListModel()
        {
            return new AchievementDataGridModel(this);
        }

        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.Achievements.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Data.School;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.Achievements
{
    public class AchievementModel : BaseModelWithLoad
    {
        internal AchievementModel(Achievement model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Achievement model)
        {
            AcademicYearId = model.AcademicYearId;
            AchievementTypeId = model.AchievementTypeId;
            LocationId = model.LocationId;
            CreatedById = model.CreatedById;
            CreatedDate = model.CreatedDate;
            Comments = model.Comments;
            Deleted = model.Deleted;

            if (model.Type != null)
            {
                Type = new AchievementTypeModel(model.Type);
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
        }

        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "Achievement Type is required.")]
        public Guid AchievementTypeId { get; set; }

        public Guid? LocationId { get; set; }
        
        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        public bool Deleted { get; set; }

        public AchievementTypeModel Type { get; set; }

        public LocationModel Location { get; set; }

        public AcademicYearModel AcademicYear { get; set; }

        public UserModel CreatedBy { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var achievement = await unitOfWork.Achievements.GetById(Id.Value);

                if (achievement != null)
                {
                    LoadFromModel(achievement);
                }
            }
        }
    }
}

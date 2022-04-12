using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AchievementModel : BaseModel, ILoadable
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

        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Achievements.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

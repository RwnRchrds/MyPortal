using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Documents;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class LessonPlanModel : BaseModelWithLoad
    {
        public LessonPlanModel(LessonPlan model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(LessonPlan model)
        {
            StudyTopicId = model.StudyTopicId;
            CreatedById = model.CreatedById;
            DirectoryId = model.DirectoryId;
            Title = model.Title;
            PlanContent = model.PlanContent;

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.StudyTopic != null)
            {
                StudyTopic = new StudyTopicModel(model.StudyTopic);
            }
        }

        public Guid StudyTopicId { get; set; }

        public Guid CreatedById { get; set; }

        public Guid DirectoryId { get; set; }

        [Required] [StringLength(256)] public string Title { get; set; }

        [Required] public string PlanContent { get; set; }

        public virtual DirectoryModel Directory { get; set; }
        public virtual UserModel CreatedBy { get; set; }
        public virtual StudyTopicModel StudyTopic { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.LessonPlans.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
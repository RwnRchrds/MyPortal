using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamAwardModel : BaseModelWithLoad
    {
        public ExamAwardModel(ExamAward model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamAward model)
        {
            QualificationId = model.QualificationId;
            AssessmentId = model.AssessmentId;
            CourseId = model.CourseId;
            Description = model.Description;
            AwardCode = model.AwardCode;
            ExpiryDate = model.ExpiryDate;

            if (model.Assessment != null)
            {
                Assessment = new ExamAssessmentModel(model.Assessment);
            }

            if (model.Qualification != null)
            {
                Qualification = new ExamQualificationModel(model.Qualification);
            }

            if (model.Course != null)
            {
                Course = new CourseModel(model.Course);
            }
        }

        public Guid QualificationId { get; set; }

        public Guid AssessmentId { get; set; }

        public Guid? CourseId { get; set; }

        public string Description { get; set; }

        public string AwardCode { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public virtual ExamAssessmentModel Assessment { get; set; }
        public virtual ExamQualificationModel Qualification { get; set; }
        public virtual CourseModel Course { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamAwards.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}
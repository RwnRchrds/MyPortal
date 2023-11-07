using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Calendar;
using MyPortal.Logic.Models.Data.StaffMembers;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Students.SEND
{
    public class SenReviewModel : BaseModelWithLoad
    {
        public SenReviewModel(SenReview model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(SenReview model)
        {
            StudentId = model.StudentId;
            ReviewTypeId = model.ReviewTypeId;
            ReviewStatusId = model.ReviewStatusId;
            SencoId = model.SencoId;
            EventId = model.EventId;
            OutcomeSenStatusId = model.OutcomeSenStatusId;
            Comments = model.Comments;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Senco != null)
            {
                Senco = new StaffMemberModel(model.Senco);
            }

            if (model.Event != null)
            {
                Event = new DiaryEventModel(model.Event);
            }

            if (model.OutcomeStatus != null)
            {
                OutcomeStatus = new SenStatusModel(model.OutcomeStatus);
            }

            if (model.ReviewStatus != null)
            {
                ReviewStatus = new SenReviewStatusModel(model.ReviewStatus);
            }

            if (model.ReviewType != null)
            {
                ReviewType = new SenReviewTypeModel(model.ReviewType);
            }
        }

        public Guid StudentId { get; set; }

        public Guid ReviewTypeId { get; set; }

        public Guid ReviewStatusId { get; set; }

        public Guid? SencoId { get; set; }

        public Guid EventId { get; set; }

        public Guid? OutcomeSenStatusId { get; set; }

        [StringLength(256)] public string Comments { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual StaffMemberModel Senco { get; set; }

        public virtual DiaryEventModel Event { get; set; }

        public virtual SenStatusModel OutcomeStatus { get; set; }

        public virtual SenReviewStatusModel ReviewStatus { get; set; }

        public virtual SenReviewTypeModel ReviewType { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.SenReviews.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
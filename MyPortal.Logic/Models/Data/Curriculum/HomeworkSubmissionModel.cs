using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Documents;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class HomeworkSubmissionModel : BaseModelWithLoad
    {
        public HomeworkSubmissionModel(HomeworkSubmission model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(HomeworkSubmission model)
        {
            HomeworkId = model.HomeworkId;
            StudentId = model.StudentId;
            TaskId = model.TaskId;
            DocumentId = model.DocumentId;
            PointsAchieved = model.PointsAchieved;
            Comments = model.Comments;

            if (model.HomeworkItem != null)
            {
                HomeworkItem = new HomeworkItemModel(model.HomeworkItem);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Task != null)
            {
                Task = new TaskModel(model.Task);
            }

            if (model.SubmittedWork != null)
            {
                SubmittedWork = new DocumentModel(model.SubmittedWork);
            }
        }

        public Guid HomeworkId { get; set; }
        public Guid StudentId { get; set; }
        public Guid TaskId { get; set; }
        public Guid? DocumentId { get; set; }
        public int PointsAchieved { get; set; }
        public string Comments { get; set; }

        public virtual HomeworkItemModel HomeworkItem { get; set; }
        public virtual StudentModel Student { get; set; }
        public virtual TaskModel Task { get; set; }
        public virtual DocumentModel SubmittedWork { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.HomeworkSubmissions.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
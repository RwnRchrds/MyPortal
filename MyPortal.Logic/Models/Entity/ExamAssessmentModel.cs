using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAssessmentModel : BaseModel, ILoadable
    {
        public ExamAssessmentModel(ExamAssessment model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamAssessment model)
        {
            ExamBoardId = model.ExamBoardId;
            AssessmentType = (int) model.AssessmentType;
            InternalTitle = model.InternalTitle;
            ExternalTitle = model.ExternalTitle;

            if (model.ExamBoard != null)
            {
                ExamBoard = new ExamBoardModel(model.ExamBoard);
            }
        }
        
        public Guid ExamBoardId { get; set; }
        
        public int AssessmentType { get; set; }
        
        public string InternalTitle { get; set; }
        
        public string ExternalTitle { get; set; }

        public virtual ExamBoardModel ExamBoard { get; set; }

        public async Task Load(IUnitOfWork unitOfWork)
        {if (Id.HasValue)
            {
                var model = await unitOfWork.ExamAssessments.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}
using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamBaseComponentModel : BaseModel, ILoadable
    {
        public ExamBaseComponentModel(ExamBaseComponent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamBaseComponent model)
        {
            AssessmentModeId = model.AssessmentModeId;
            ExamAssessmentId = model.ExamAssessmentId;
            ComponentCode = model.ComponentCode;

            if (model.AssessmentMode != null)
            {
                AssessmentMode = new ExamAssessmentModeModel(model.AssessmentMode);
            }

            if (model.Assessment != null)
            {
                Assessment = new ExamAssessmentModel(model.Assessment);
            }
        }
        
        public Guid AssessmentModeId { get; set; }
        
        public Guid ExamAssessmentId { get; set; }
        
        public string ComponentCode { get; set; }

        public virtual ExamAssessmentModeModel AssessmentMode { get; set; }
        public virtual ExamAssessmentModel Assessment { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ExamBaseComponents.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}
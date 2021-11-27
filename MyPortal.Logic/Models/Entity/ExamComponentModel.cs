using System;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamComponentModel : BaseModel, ILoadable
    {
        public ExamComponentModel(ExamComponent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamComponent model)
        {
            BaseComponentId = model.BaseComponentId;
            ExamSeriesId = model.ExamSeriesId;
            AssessmentModeId = model.AssessmentModeId;
            ExamDateId = model.ExamDateId;
            DateDue = model.DateDue;
            DateSubmitted = model.DateSubmitted;
            MaximumMark = model.MaximumMark;

            if (model.BaseComponent != null)
            {
                BaseComponent = new ExamBaseComponentModel(model.BaseComponent);
            }

            if (model.Series != null)
            {
                Series = new ExamSeriesModel(model.Series);
            }

            if (model.AssessmentMode != null)
            {
                AssessmentMode = new ExamAssessmentModeModel(model.AssessmentMode);
            }

            if (model.ExamDate != null)
            {
                ExamDate = new ExamDateModel(model.ExamDate);
            }
        }

        public Guid BaseComponentId { get; set; }
        
        public Guid ExamSeriesId { get; set; }
        
        public Guid AssessmentModeId { get; set; }

        public Guid? ExamDateId { get; set; }

        
        public DateTime? DateDue { get; set; }

        
        public DateTime? DateSubmitted { get; set; }


        public bool IsTimetabled => ExamDateId.HasValue;

        
        public int MaximumMark { get; set; }

        public virtual ExamBaseComponentModel BaseComponent { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
        public virtual ExamAssessmentModeModel AssessmentMode { get; set; }
        public virtual ExamDateModel ExamDate { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamComponents.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
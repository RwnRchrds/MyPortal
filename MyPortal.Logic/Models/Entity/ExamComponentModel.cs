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
            DateDue = model.DateDue;
            DateSubmitted = model.DateSubmitted;
            IsTimetabled = model.IsTimetabled;
            MaximumMark = model.MaximumMark;
            SessionId = model.SessionId;
            Duration = model.Duration;
            SittingDate = model.SittingDate;
            ExamSessionId = model.SessionId;

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

            if (model.Session != null)
            {
                Session = new ExamSessionModel(model.Session);
            }
        }

        public Guid BaseComponentId { get; set; }
        
        public Guid ExamSeriesId { get; set; }
        
        public Guid AssessmentModeId { get; set; }

        
        public DateTime? DateDue { get; set; }

        
        public DateTime? DateSubmitted { get; set; }

        
        public bool IsTimetabled { get; set; }

        
        public int MaximumMark { get; set; }
        
        #region Examination Specific Data

        
        public Guid? SessionId { get; set; }

        
        public int? Duration { get; set; }

        
        public DateTime? SittingDate { get; set; }

        
        public Guid? ExamSessionId { get; set; }

        #endregion

        public virtual ExamBaseComponentModel BaseComponent { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
        public virtual ExamAssessmentModeModel AssessmentMode { get; set; }
        public virtual ExamSessionModel Session { get; set; }
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
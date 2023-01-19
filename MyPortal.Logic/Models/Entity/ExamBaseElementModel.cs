using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamBaseElementModel : BaseModelWithLoad
    {
        public ExamBaseElementModel(ExamBaseElement model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamBaseElement model)
        {
            AssessmentId = model.AssessmentId;
            LevelId = model.LevelId;
            QcaCodeId = model.QcaCodeId;
            QualAccrNumber = model.QualAccrNumber;
            ElementCode = model.ElementCode;

            if (model.Assessment != null)
            {
                Assessment = new ExamAssessmentModel(model.Assessment);
            }

            if (model.QcaCode != null)
            {
                QcaCode = new SubjectCodeModel(model.QcaCode);
            }

            if (model.Level != null)
            {
                Level = new ExamQualificationLevelModel(model.Level);
            }
        }
        
        public Guid AssessmentId { get; set; }
        
        public Guid LevelId { get; set; }
        
        public Guid QcaCodeId { get; set; }
        
        public string QualAccrNumber { get; set; }
        
        public string ElementCode { get; set; }

        public virtual ExamAssessmentModel Assessment { get; set; }
        public virtual SubjectCodeModel QcaCode { get; set; }
        public virtual ExamQualificationLevelModel Level { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamBaseElements.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamQualificationLevelModel : LookupItemModel, ILoadable
    {
        public Guid QualificationId { get; set; }
        
        public Guid? DefaultGradeSetId { get; set; }
        
        [StringLength(25)]
        public string JcLevelCode { get; set; }

        public virtual GradeSetModel DefaultGradeSet { get; set; }
        public virtual ExamQualificationModel Qualification { get; set; }

        public ExamQualificationLevelModel(ExamQualificationLevel model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamQualificationLevel model)
        {
            QualificationId = model.QualificationId;
            DefaultGradeSetId = model.DefaultGradeSetId;
            JcLevelCode = model.JcLevelCode;

            if (model.DefaultGradeSet != null)
            {
                DefaultGradeSet = new GradeSetModel(model.DefaultGradeSet);
            }

            if (model.Qualification != null)
            {
                Qualification = new ExamQualificationModel(model.Qualification);
            }
        }

        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamQualificationLevels.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Assessment;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamResultEmbargoModel : BaseModelWithLoad
    {
        public ExamResultEmbargoModel(ExamResultEmbargo model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamResultEmbargo model)
        {
            ResultSetId = model.ResultSetId;
            EndTime = model.EndTime;

            if (model.ResultSet != null)
            {
                ResultSet = new ResultSetModel(model.ResultSet);
            }
        }
        
        public Guid ResultSetId { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamResultEmbargoes.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
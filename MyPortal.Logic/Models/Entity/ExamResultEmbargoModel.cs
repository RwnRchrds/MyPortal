using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamResultEmbargoModel : BaseModel, ILoadable
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
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ExamResultEmbargoes.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}
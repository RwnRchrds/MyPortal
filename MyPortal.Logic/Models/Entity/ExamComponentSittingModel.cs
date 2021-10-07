using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamComponentSittingModel : BaseModel, ILoadable
    {
        public ExamComponentSittingModel(ExamComponentSitting model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamComponentSitting model)
        {
            ComponentId = model.ComponentId;
            ExamRoomId = model.ExamRoomId;
            ExamDate = model.ExamDate;
            ActualStartTime = model.ActualStartTime;
            ExtraTimePercent = model.ExtraTimePercent;

            if (model.Component != null)
            {
                Component = new ExamComponentModel(model.Component);
            }

            if (model.Room != null)
            {
                Room = new ExamRoomModel(model.Room);
            }
        }
        
        public Guid ComponentId { get; set; }
        
        public Guid ExamRoomId { get; set; }
        
        public DateTime ExamDate { get; set; }
        
        public TimeSpan? ActualStartTime { get; set; }
        
        public int ExtraTimePercent { get; set; }

        public virtual ExamComponentModel Component { get; set; }
        public virtual ExamRoomModel Room { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ExamComponentSittings.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}
using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamComponentSittingModel : BaseModelWithLoad
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
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamComponentSittings.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
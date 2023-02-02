using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamDateModel : BaseModelWithLoad
    {
        public ExamDateModel(ExamDate model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamDate model)
        {
            SessionId = model.SessionId;
            Duration = model.Duration;
            SittingDate = model.SittingDate;

            if (model.Session != null)
            {
                Session = new ExamSessionModel(model.Session);
            }
        }
        
        public Guid SessionId { get; set; }
        
        public int Duration { get; set; }
        
        public DateTime SittingDate { get; set; }

        public virtual ExamSessionModel Session { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamDates.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
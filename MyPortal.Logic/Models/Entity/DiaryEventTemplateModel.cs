using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class DiaryEventTemplateModel : LookupItemModel, ILoadable
    {
        public Guid EventTypeId { get; set; }
        
        public int Minutes { get; set; }
        
        public int Hours { get; set; }
        
        public int Days { get; set; }
        
        public DiaryEventTypeModel DiaryEventType { get; set; }

        public DiaryEventTemplateModel(DiaryEventTemplate model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(DiaryEventTemplate model)
        {
            EventTypeId = model.EventTypeId;
            Minutes = model.Minutes;
            Hours = model.Hours;
            Days = model.Days;

            if (model.DiaryEventType != null)
            {
                DiaryEventType = new DiaryEventTypeModel(model.DiaryEventType);
            }
        }

        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.DiaryEventTemplates.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
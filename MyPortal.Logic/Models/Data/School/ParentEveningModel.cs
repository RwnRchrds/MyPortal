using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Calendar;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.School
{
    public class ParentEveningModel : BaseModelWithLoad
    {
        public ParentEveningModel(ParentEvening model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ParentEvening model)
        {
            EventId = model.EventId;
            Name = model.Name;
            BookingOpened = model.BookingOpened;
            BookingClosed = model.BookingClosed;

            if (model.Event != null)
            {
                Event = new DiaryEventModel(model.Event);
            }
        }
        
        public Guid EventId { get; set; }
        
        [StringLength(128)]
        public string Name { get; set; }
        
        public DateTime BookingOpened { get; set; }
        
        public DateTime BookingClosed { get; set; } 

        public virtual DiaryEventModel Event { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ParentEvenings.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
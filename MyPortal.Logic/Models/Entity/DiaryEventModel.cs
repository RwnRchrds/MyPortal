using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class DiaryEventModel : BaseModel, ILoadable
    {
        public DiaryEventModel(DiaryEvent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(DiaryEvent model)
        {
            EventTypeId = model.EventTypeId;
            RoomId = model.RoomId;
            Subject = model.Subject;
            Description = model.Description;
            Location = model.Location;
            StartTime = model.StartTime;
            EndTime = model.EndTime;
            IsAllDay = model.IsAllDay;
            IsBlock = model.IsBlock;
            IsPublic = model.IsPublic;

            if (model.EventType != null)
            {
                EventType = new DiaryEventTypeModel(model.EventType);
            }

            if (model.Room != null)
            {
                Room = new RoomModel(model.Room);
            }
        }
        
        public Guid EventTypeId { get; set; }
        
        public Guid? RoomId { get; set; }
        
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }
        
        [StringLength(256)]
        public string Location { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public bool IsAllDay { get; set; }
        
        public bool IsBlock { get; set; }
        
        public bool IsPublic { get; set; }

        public virtual DiaryEventTypeModel EventType { get; set; }
        public virtual RoomModel Room { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.DiaryEvents.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
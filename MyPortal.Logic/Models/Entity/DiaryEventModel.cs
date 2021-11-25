using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        internal async Task<bool> CanEdit(IUnitOfWork unitOfWork, Guid personId)
        {
            if (Id.HasValue)
            {
                var attendees = await unitOfWork.DiaryEventAttendees.GetByEvent(Id.Value);

                var attendeePerson = attendees.FirstOrDefault(a => a.PersonId == personId);

                if (attendeePerson != null)
                {
                    return attendeePerson.CanEdit;
                }
            }

            return false;
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
using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class DiaryEventAttendeeModel : BaseModel, ILoadable
    {
        public DiaryEventAttendeeModel(DiaryEventAttendee model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(DiaryEventAttendee model)
        {
            EventId = model.EventId;
            PersonId = model.PersonId;
            ResponseId = model.ResponseId;
            Required = model.Required;
            Attended = model.Attended;

            if (model.Event != null)
            {
                Event = new DiaryEventModel(model.Event);
            }

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }

            if (model.Response != null)
            {
                Response = new DiaryEventAttendeeResponseModel(model.Response);
            }
        }
            
        
        public Guid EventId { get; set; }
        
        public Guid PersonId { get; set; }
        
        public Guid? ResponseId { get; set; }
        
        public bool Required { get; set; }
        
        public bool Attended { get; set; }

        public virtual DiaryEventModel Event { get; set; }
        public virtual PersonModel Person { get; set; }
        public virtual DiaryEventAttendeeResponseModel Response { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.DiaryEventAttendees.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}
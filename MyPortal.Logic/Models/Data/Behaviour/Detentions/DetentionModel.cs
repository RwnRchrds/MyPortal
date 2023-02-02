using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Calendar;
using MyPortal.Logic.Models.Data.StaffMembers;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.Detentions
{
    public class DetentionModel : BaseModelWithLoad
    {
        public DetentionModel(Detention model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Detention model)
        {
            DetentionTypeId = model.DetentionTypeId;
            EventId = model.EventId;
            SupervisorId = model.SupervisorId;

            if (model.Type != null)
            {
                Type = new DetentionTypeModel(model.Type);
            }

            if (model.Event != null)
            {
                Event = new DiaryEventModel(model.Event);
            }

            if (model.Supervisor != null)
            {
                Supervisor = new StaffMemberModel(model.Supervisor);
            }
        }
        
        public Guid DetentionTypeId { get; set; }
        
        public Guid EventId { get; set; }
        
        public Guid? SupervisorId { get; set; }

        public DetentionTypeModel Type { get; set; }
        public DiaryEventModel Event { get; set; }
        public StaffMemberModel Supervisor { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Detentions.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
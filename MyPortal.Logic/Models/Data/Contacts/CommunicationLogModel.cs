using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Contacts
{
    public class CommunicationLogModel : BaseModelWithLoad
    {
        public CommunicationLogModel(CommunicationLog model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(CommunicationLog model)
        {
            ContactId = model.ContactId;
            CommunicationTypeId = model.CommunicationTypeId;
            Date = model.Date;
            Notes = model.Notes;
            Outgoing = model.Outgoing;

            if (model.Type != null)
            {
                Type = new CommunicationTypeModel(model.Type);
            }

            if (model.Contact != null)
            {
                Contact = new ContactModel(model.Contact);
            }
        }
        
        public Guid ContactId { get; set; }
        
        public Guid CommunicationTypeId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Notes { get; set; }
        
        public bool Outgoing { get; set; }

        public CommunicationTypeModel Type { get; set; }
        public ContactModel Contact { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var commLog = await unitOfWork.CommunicationLogs.GetById(Id.Value);

                if (commLog != null)
                {
                    LoadFromModel(commLog);
                }
            }
        }
    }
}
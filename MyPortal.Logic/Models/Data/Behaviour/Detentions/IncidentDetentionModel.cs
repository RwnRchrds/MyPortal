using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.Detentions
{
    public class IncidentDetentionModel : BaseModelWithLoad
    {
        public IncidentDetentionModel(StudentIncidentDetention model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(StudentIncidentDetention model)
        {
            StudentIncidentId = model.StudentIncidentId;
            DetentionId = model.DetentionId;

            if (model.StudentIncident != null)
            {
                Incident = new StudentIncidentModel(model.StudentIncident);
            }

            if (model.Detention != null)
            {
                Detention = new DetentionModel(model.Detention);
            }
        }
        
        public Guid StudentIncidentId { get; set; }

        public Guid DetentionId { get; set; }

        public virtual StudentIncidentModel Incident { get; set; }
        public virtual DetentionModel Detention { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.IncidentDetentions.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

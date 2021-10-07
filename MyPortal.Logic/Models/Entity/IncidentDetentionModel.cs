using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class IncidentDetentionModel : BaseModel, ILoadable
    {
        public IncidentDetentionModel(IncidentDetention model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(IncidentDetention model)
        {
            IncidentId = model.IncidentId;
            DetentionId = model.DetentionId;

            if (model.Incident != null)
            {
                Incident = new IncidentModel(model.Incident);
            }

            if (model.Detention != null)
            {
                Detention = new DetentionModel(model.Detention);
            }
        }
        
        public Guid IncidentId { get; set; }

        public Guid DetentionId { get; set; }

        public virtual IncidentModel Incident { get; set; }
        public virtual DetentionModel Detention { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.IncidentDetentions.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

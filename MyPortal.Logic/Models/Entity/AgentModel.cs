using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AgentModel : BaseModel, ILoadable
    {
        public AgentModel(Agent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Agent model)
        {
            AgencyId = model.AgencyId;
            PersonId = model.PersonId;
            AgentTypeId = model.AgentTypeId;
            JobTitle = model.JobTitle;
            Deleted = model.Deleted;

            if (model.Agency != null)
            {
                Agency = new AgencyModel(model.Agency);
            }

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }

            if (model.AgentType != null)
            {
                AgentType = new AgentTypeModel(model.AgentType);
            }
        }
        
        public Guid AgencyId { get; set; }

        public Guid PersonId { get; set; }

        public Guid AgentTypeId { get; set; }

        [StringLength(128)]
        public string JobTitle { get; set; }

        public bool Deleted { get; set; }

        public virtual AgencyModel Agency { get; set; }
        public virtual PersonModel Person { get; set; }
        public virtual AgentTypeModel AgentType { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Agents.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

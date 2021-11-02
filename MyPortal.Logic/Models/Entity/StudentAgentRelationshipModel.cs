using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentAgentRelationshipModel : BaseModel, ILoadable
    {
        public StudentAgentRelationshipModel(StudentAgentRelationship model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(StudentAgentRelationship model)
        {
            StudentId = model.StudentId;
            AgentId = model.AgentId;
            RelationshipTypeId = model.RelationshipTypeId;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Agent != null)
            {
                Agent = new AgentModel(model.Agent);
            }

            if (model.RelationshipType != null)
            {
                RelationshipType = new RelationshipTypeModel(model.RelationshipType);
            }
        }
        
        public Guid StudentId { get; set; }
        
        public Guid AgentId { get; set; }
        
        public Guid RelationshipTypeId { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual AgentModel Agent { get; set; }
        public virtual RelationshipTypeModel RelationshipType { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.StudentAgentRelationships.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
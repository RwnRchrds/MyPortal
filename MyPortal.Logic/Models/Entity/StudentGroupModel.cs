using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentGroupModel : BaseModel, ILoadable
    {
        public StudentGroupModel(StudentGroup model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(StudentGroup model)
        {
            Code = model.Code;
            Name = model.Name;
            PromoteToGroupId = model.PromoteToGroupId;
            MaxMembers = model.MaxMembers;
            Notes = model.Notes;
            System = model.System;

            if (model.PromoteToGroup != null)
            {
                PromoteToGroup = new StudentGroupModel(model.PromoteToGroup);
            }
        }
        
        public string Code { get; set; }
        public string Name { get; set; }

        public Guid? PromoteToGroupId { get; set; }
        
        public int? MaxMembers { get; set; }
        
        public string Notes { get; set; }
        
        public bool System { get; set; }
        
        public StudentGroupModel PromoteToGroup { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.StudentGroups.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
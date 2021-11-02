using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class SystemAreaModel : BaseModel, ILoadable
    {
        public SystemAreaModel(SystemArea model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(SystemArea model)
        {
            Description = model.Description;
            ParentId = model.ParentId;

            if (model.Parent != null)
            {
                Parent = new SystemAreaModel(model.Parent);
            }
        }
        
        [Required]
        [StringLength(128)]
        public string Description { get; set; }
        
        public Guid? ParentId { get; set; }

        public virtual SystemAreaModel Parent { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.SystemAreas.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
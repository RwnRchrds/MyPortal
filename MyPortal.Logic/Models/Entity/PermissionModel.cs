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
    public class PermissionModel : BaseModel, ILoadable
    {
        public PermissionModel(Permission model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Permission model)
        {
            AreaId = model.AreaId;
            ShortDescription = model.ShortDescription;
            FullDescription = model.FullDescription;

            if (model.SystemArea != null)
            {
                SystemArea = new SystemAreaModel(model.SystemArea);
            }
        }
        
        public Guid AreaId { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public virtual SystemAreaModel SystemArea { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Permissions.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

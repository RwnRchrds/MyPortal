using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class DirectoryModel : BaseModel, ILoadableTree
    {
        public DirectoryModel(Directory model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Directory model)
        {
            ParentId = model.ParentId;
            Name = model.Name;
            Restricted = model.Restricted;

            if (model.Parent != null)
            {
                Parent = new DirectoryModel(model.Parent);
            }
        }
        
        public Guid? ParentId { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Restricted { get; set; } 

        public virtual DirectoryModel Parent { get; set; }


        public async Task Load(IUnitOfWork unitOfWork, bool deep = false)
        {
            var model = await unitOfWork.Directories.GetById(Id);
            
            LoadFromModel(model);

            if (deep && Parent != null)
            {
                await Parent.Load(unitOfWork, true);
            }
        }

        public async Task<DirectoryModel> GetParent(IUnitOfWork unitOfWork)
        {
            if (ParentId.HasValue)
            {
                var parent = await unitOfWork.Directories.GetById(ParentId.Value);

                return new DirectoryModel(parent);
            }

            return null;
        }
    }
}

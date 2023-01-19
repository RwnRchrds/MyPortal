using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class DirectoryModel : BaseModelWithTreeLoad
    {
        public DirectoryModel(Directory model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Directory model)
        {
            ParentId = model.ParentId;
            Name = model.Name;
            Private = model.Private;

            if (model.Parent != null)
            {
                ParentModel = new DirectoryModel(model.Parent);
            }
        }

        public DirectoryModel Parent => ParentModel as DirectoryModel;
        
        public Guid? ParentId { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public bool Private { get; set; }


        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Directories.GetById(Id.Value);
            
                LoadFromModel(model);
            }
        }

        public async Task<DirectoryModel> GetParent(IUnitOfWork unitOfWork)
        {
            if (ParentModel != null)
            {
                return Parent;
            }
            
            if (ParentId.HasValue)
            {
                var parent = await unitOfWork.Directories.GetById(ParentId.Value);

                return new DirectoryModel(parent);
            }

            return null;
        }
        
        public DirectoryChildSummaryModel GetListModel()
        {
            return new DirectoryChildSummaryModel(this);
        }
    }
}

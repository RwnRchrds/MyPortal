using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AgencyModel : BaseModel, ILoadable
    {
        public AgencyModel(Agency model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Agency model)
        {
            TypeId = model.TypeId;
            AddressId = model.AddressId;
            DirectoryId = model.DirectoryId;
            Website = model.Website;
            Deleted = model.Deleted;

            if (model.AgencyType != null)
            {
                AgencyType = new AgencyTypeModel(model.AgencyType);
            }

            if (model.Address != null)
            {
                Address = new AddressModel(model.Address);
            }

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }
        }
        
        public Guid TypeId { get; set; }

        public Guid? AddressId { get; set; }

        public Guid DirectoryId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [Url]
        [StringLength(100)]
        public string Website { get; set; }

        public bool Deleted { get; set; }

        public virtual AgencyTypeModel AgencyType { get; set; }
        public virtual AddressModel Address { get; set; }
        public virtual DirectoryModel Directory { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Agencies.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

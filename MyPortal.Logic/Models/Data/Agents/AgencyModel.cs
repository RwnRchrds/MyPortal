using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Documents;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Agents
{
    public class AgencyModel : BaseModelWithLoad
    {
        public AgencyModel(Agency model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Agency model)
        {
            TypeId = model.TypeId;
            DirectoryId = model.DirectoryId;
            Website = model.Website;
            Deleted = model.Deleted;

            if (model.AgencyType != null)
            {
                AgencyType = new AgencyTypeModel(model.AgencyType);
            }

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }
        }
        
        public Guid TypeId { get; set; }

        public Guid DirectoryId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [Url]
        [StringLength(100)]
        public string Website { get; set; }

        public bool Deleted { get; set; }

        public virtual AgencyTypeModel AgencyType { get; set; }
        public virtual DirectoryModel Directory { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var agency = await unitOfWork.Agencies.GetById(Id.Value);

                if (agency != null)
                {
                    LoadFromModel(agency);
                }
            }
        }
    }
}

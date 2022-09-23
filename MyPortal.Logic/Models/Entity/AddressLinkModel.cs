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
    public class AddressLinkModel : BaseModel, ILoadable
    {
        public AddressLinkModel(AddressLink model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AddressLink model)
        {
            AddressId = model.AddressId;
            PersonId = model.PersonId;
            AgencyId = model.AgencyId;

            if (model.Address != null)
            {
                Address = new AddressModel(model.Address);
            }

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }

            if (model.Agency != null)
            {
                Agency = new AgencyModel(model.Agency);
            }
        }
        
        public Guid AddressId { get; set; }

        public Guid? PersonId { get; set; }
        
        public Guid? AgencyId { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual PersonModel Person { get; set; }
        public virtual AgencyModel Agency { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.AddressLinks.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

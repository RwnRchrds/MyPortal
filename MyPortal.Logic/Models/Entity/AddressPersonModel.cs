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
    public class AddressPersonModel : BaseModel, ILoadable
    {
        public AddressPersonModel(AddressPerson model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AddressPerson model)
        {
            AddressId = model.AddressId;
            PersonId = model.PersonId;

            if (model.Address != null)
            {
                Address = new AddressModel(model.Address);
            }

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }
        }
        
        public Guid AddressId { get; set; }

        public Guid PersonId { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual PersonModel Person { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.AddressPersons.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

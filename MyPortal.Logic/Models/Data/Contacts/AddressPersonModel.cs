using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Contacts
{
    public class AddressPersonModel : BaseModelWithLoad
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

        public Guid? PersonId { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual PersonModel Person { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var addressPerson = await unitOfWork.AddressPeople.GetById(Id.Value);

                if (addressPerson != null)
                {
                    LoadFromModel(addressPerson);
                }
            }
        }
    }
}

using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Medical;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.People
{
    public class PersonDietaryRequirementModel : BaseModelWithLoad
    {
        public PersonDietaryRequirementModel(PersonDietaryRequirement model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(PersonDietaryRequirement model)
        {
            PersonId = model.PersonId;
            DietaryRequirementId = model.DietaryRequirementId;

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }

            if (model.DietaryRequirement != null)
            {
                DietaryRequirement = new DietaryRequirementModel(model.DietaryRequirement);
            }
        }
        
        public Guid PersonId { get; set; }

        public Guid DietaryRequirementId { get; set; }

        public virtual DietaryRequirementModel DietaryRequirement { get; set; }
        public virtual PersonModel Person { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.PersonDietaryRequirements.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

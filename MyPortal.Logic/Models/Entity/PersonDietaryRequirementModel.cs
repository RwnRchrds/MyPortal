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
    public class PersonDietaryRequirementModel : BaseModel, ILoadable
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
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.PersonDietaryRequirements.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

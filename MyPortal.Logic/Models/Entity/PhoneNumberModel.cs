using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class PhoneNumberModel : BaseModel, ILoadable
    {
        public PhoneNumberModel(PhoneNumber model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(PhoneNumber model)
        {
            TypeId = model.TypeId;
            PersonId = model.PersonId;
            AgencyId = model.AgencyId;
            Number = model.Number;
            Main = model.Main;

            if (model.Type != null)
            {
                Type = new PhoneNumberTypeModel(model.Type);
            }

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }
        }
        
        public Guid TypeId { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? AgencyId { get; set; }

        [Phone]
        [StringLength(128)]
        public string Number { get; set; }

        public bool Main { get; set; }

        public virtual PhoneNumberTypeModel Type { get; set; }
        public virtual PersonModel Person { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.PhoneNumbers.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

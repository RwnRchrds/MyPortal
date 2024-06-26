﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Contacts
{
    public class EmailAddressModel : BaseModelWithLoad
    {
        public EmailAddressModel(EmailAddress model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(EmailAddress model)
        {
            TypeId = model.TypeId;
            PersonId = model.PersonId;
            AgencyId = model.AgencyId;
            Address = model.Address;
            Main = model.Main;
            Notes = model.Notes;

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }

            if (model.Type != null)
            {
                Type = new EmailAddressTypeModel(model.Type);
            }
        }

        public Guid TypeId { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? AgencyId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }

        public bool Main { get; set; }

        public string Notes { get; set; }

        public virtual PersonModel Person { get; set; }
        public virtual EmailAddressTypeModel Type { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.EmailAddresses.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}
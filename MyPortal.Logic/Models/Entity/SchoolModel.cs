﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class SchoolModel : BaseModel, ILoadable
    {
        public SchoolModel(School model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(School model)
        {
            Name = model.Name;
            LocalAuthorityId = model.LocalAuthorityId;
            EstablishmentNumber = model.EstablishmentNumber;
            Urn = model.Urn;
            Uprn = model.Uprn;
            PhaseId = model.PhaseId;
            TypeId = model.TypeId;
            GovernanceTypeId = model.GovernanceTypeId;
            IntakeTypeId = model.IntakeTypeId;
            HeadTeacherId = model.HeadTeacherId;
            TelephoneNo = model.TelephoneNo;
            FaxNo = model.FaxNo;
            EmailAddress = model.EmailAddress;
            Website = model.Website;
            Local = model.Local;

            if (model.SchoolPhase != null)
            {
                SchoolPhase = new SchoolPhaseModel(model.SchoolPhase);
            }

            if (model.Type != null)
            {
                Type = new SchoolTypeModel(model.Type);
            }

            if (model.GovernanceType != null)
            {
                GovernanceType = new GovernanceTypeModel(model.GovernanceType);
            }

            if (model.IntakeType != null)
            {
                IntakeType = new IntakeTypeModel(model.IntakeType);
            }

            if (model.HeadTeacher != null)
            {
                HeadTeacher = new PersonModel(model.HeadTeacher);
            }

            if (model.LocalAuthority != null)
            {
                LocalAuthority = new LocalAuthorityModel(model.LocalAuthority);
            }
        }
        
        public string Name { get; set; }
        
        public Guid? LocalAuthorityId { get; set; }
        
        public int EstablishmentNumber { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Urn { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Uprn { get; set; }
        
        public Guid PhaseId { get; set; }
        
        public Guid TypeId { get; set; }
        
        public Guid GovernanceTypeId { get; set; }
        
        public Guid IntakeTypeId { get; set; }
        
        public Guid? HeadTeacherId { get; set; }
        
        [Phone]
        [StringLength(128)]
        public string TelephoneNo { get; set; }
        
        [Phone]
        [StringLength(128)]
        public string FaxNo { get; set; }
        
        [EmailAddress]
        [StringLength(128)]
        public string EmailAddress { get; set; }
        
        [Url]
        [StringLength(128)]
        public string Website { get; set; }
        
        public bool Local { get; set; }

        public virtual SchoolPhaseModel SchoolPhase { get; set; }
        public virtual SchoolTypeModel Type { get; set; }
        public virtual GovernanceTypeModel GovernanceType { get; set; }
        public virtual IntakeTypeModel IntakeType { get; set; }
        public virtual PersonModel HeadTeacher { get; set; }
        public virtual LocalAuthorityModel LocalAuthority { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Schools.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
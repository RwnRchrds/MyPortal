using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.StaffMembers
{
    public class StaffMemberModel : BaseModelWithLoad
    {
        public StaffMemberModel(StaffMember model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(StaffMember model)
        {
            Id = model.Id;
            LineManagerId = model.LineManagerId;
            PersonId = model.PersonId;
            Code = model.Code;
            BankName = model.BankName;
            BankAccount = model.BankAccount;
            BankSortCode = model.BankSortCode;
            NiNumber = model.NiNumber;
            Qualifications = model.Qualifications;
            TeachingStaff = model.TeachingStaff;
            Deleted = model.Deleted;

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }

            if (model.LineManager != null)
            {
                LineManager = new StaffMemberModel(model.LineManager);
            }
        }

        public Guid? LineManagerId { get; set; }

        public Guid PersonId { get; set; }

        [Required] [StringLength(128)] public string Code { get; set; }

        [StringLength(50)] public string BankName { get; set; }

        [StringLength(15)] public string BankAccount { get; set; }

        [StringLength(10)] public string BankSortCode { get; set; }

        [StringLength(9)] public string NiNumber { get; set; }

        [StringLength(128)] public string Qualifications { get; set; }

        public bool TeachingStaff { get; set; }

        public bool Deleted { get; set; }

        public virtual PersonModel Person { get; set; }

        public virtual StaffMemberModel LineManager { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.StaffMembers.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
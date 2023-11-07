using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.School;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.StaffMembers
{
    public class ParentEveningStaffMemberModel : BaseModelWithLoad
    {
        public ParentEveningStaffMemberModel(ParentEveningStaffMember model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ParentEveningStaffMember model)
        {
            ParentEveningId = model.ParentEveningId;
            StaffMemberId = model.StaffMemberId;
            AvailableFrom = model.AvailableFrom;
            AvailableTo = model.AvailableTo;
            AppointmentLength = model.AppointmentLength;
            BreakLimit = model.BreakLimit;

            if (model.ParentEvening != null)
            {
                ParentEvening = new ParentEveningModel(model.ParentEvening);
            }

            if (model.StaffMember != null)
            {
                StaffMember = new StaffMemberModel(model.StaffMember);
            }
        }

        public Guid ParentEveningId { get; set; }

        public Guid StaffMemberId { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public int AppointmentLength { get; set; }

        public int BreakLimit { get; set; }

        public virtual ParentEveningModel ParentEvening { get; set; }
        public virtual StaffMemberModel StaffMember { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ParentEveningStaffMembers.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
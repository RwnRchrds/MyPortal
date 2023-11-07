using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.StaffMembers;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Students
{
    public class ParentEveningAppointmentModel : BaseModelWithLoad
    {
        public ParentEveningAppointmentModel(ParentEveningAppointment model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ParentEveningAppointment model)
        {
            ParentEveningStaffId = model.ParentEveningStaffId;
            StudentId = model.StudentId;
            Start = model.Start;
            End = model.End;
            Attended = model.Attended;

            if (model.ParentEveningStaffMember != null)
            {
                ParentEveningStaffMember = new ParentEveningStaffMemberModel(model.ParentEveningStaffMember);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }
        }

        public Guid ParentEveningStaffId { get; set; }

        public Guid StudentId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool? Attended { get; set; }

        public ParentEveningStaffMemberModel ParentEveningStaffMember { get; set; }
        public StudentModel Student { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ParentEveningAppointments.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
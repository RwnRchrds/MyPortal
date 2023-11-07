using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.School;
using MyPortal.Logic.Models.Data.StaffMembers;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class SessionModel : BaseModelWithLoad
    {
        public SessionModel(Session model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Session model)
        {
            ClassId = model.ClassId;
            TeacherId = model.TeacherId;
            RoomId = model.RoomId;
            StartDate = model.StartDate;
            EndDate = model.EndDate;

            if (model.Teacher != null)
            {
                Teacher = new StaffMemberModel(model.Teacher);
            }

            if (model.Class != null)
            {
                Class = new ClassModel(model.Class);
            }

            if (model.Room != null)
            {
                Room = new RoomModel(model.Room);
            }
        }

        public Guid ClassId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid? RoomId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual StaffMemberModel Teacher { get; set; }

        public virtual ClassModel Class { get; set; }

        public virtual RoomModel Room { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Sessions.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
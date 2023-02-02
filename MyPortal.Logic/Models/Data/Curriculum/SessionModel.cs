using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Attendance;
using MyPortal.Logic.Models.Data.School;
using MyPortal.Logic.Models.Data.StaffMembers;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class SessionModel : BaseModel
    {
        public SessionModel(Session model) : base(model)
        {
            ClassId = model.ClassId;
            PeriodId = model.PeriodId;
            TeacherId = model.TeacherId;
            RoomId = model.RoomId;
            StartDate = model.StartDate;
            EndDate = model.EndDate;

            if (model.Teacher != null)
            {
                Teacher = new StaffMemberModel(model.Teacher);
            }

            if (model.AttendancePeriod != null)
            {
                AttendancePeriod = new AttendancePeriodModel(model.AttendancePeriod);
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
        
        public Guid PeriodId { get; set; }
        
        public Guid TeacherId { get; set; }
        
        public Guid? RoomId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public virtual StaffMemberModel Teacher { get; set; }
        
        public virtual AttendancePeriodModel AttendancePeriod { get; set; }

        public virtual ClassModel Class { get; set; }

        public virtual RoomModel Room { get; set; }
    }
}
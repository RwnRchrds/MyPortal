using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class CoverArrangementModel : BaseModel, ILoadable
    {
        public CoverArrangementModel(CoverArrangement model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(CoverArrangement model)
        {
            WeekId = model.WeekId;
            SessionId = model.SessionId;
            TeacherId = model.TeacherId;
            RoomId = model.RoomId;
            Comments = model.Comments;

            if (model.Week != null)
            {
                Week = new AttendanceWeekModel(model.Week);
            }

            if (model.Session != null)
            {
                Session = new SessionModel(model.Session);
            }

            if (model.Teacher != null)
            {
                Teacher = new StaffMemberModel(model.Teacher);
            }

            if (model.Room != null)
            {
                Room = new RoomModel(model.Room);
            }
        }
        
        public Guid WeekId { get; set; }
        
        public Guid SessionId { get; set; }
        
        public Guid? TeacherId { get; set; }
        
        public Guid? RoomId { get; set; }
        
        public string Comments { get; set; }

        public AttendanceWeekModel Week { get; set; }
        public SessionModel Session { get; set; }
        public StaffMemberModel Teacher { get; set; }
        public RoomModel Room { get; set; }

        public bool StaffChanged => TeacherId.HasValue;

        public bool RoomChanged => RoomId.HasValue;
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.CoverArrangements.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}
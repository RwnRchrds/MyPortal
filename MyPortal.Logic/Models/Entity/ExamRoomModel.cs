using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamRoomModel : BaseModel, ILoadable
    {
        public ExamRoomModel(ExamRoom model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamRoom model)
        {
            RoomId = model.RoomId;
            Columns = model.Columns;
            Rows = model.Rows;

            if (model.Room != null)
            {
                Room = new RoomModel(model.Room);
            }
        }

        public Guid RoomId { get; set; }
        
        public int Columns { get; set; }
        
        public int Rows { get; set; }

        public virtual RoomModel Room { get; set; }


        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamRooms.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
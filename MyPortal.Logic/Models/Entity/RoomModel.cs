using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class RoomModel : BaseModel, ILoadable
    {
        public RoomModel(Room model) : base(model)
        {
           LoadFromModel(model);
        }

        private void LoadFromModel(Room model)
        {
            LocationId = model.LocationId;
            Code = model.Code;
            Name = model.Name;
            MaxGroupSize = model.MaxGroupSize;
            TelephoneNo = model.TelephoneNo;
            ExcludeFromCover = model.ExcludeFromCover;

            if (model.Location != null)
            {
                Location = new LocationModel(model.Location);
            }
        }
        
        public Guid? LocationId { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public int MaxGroupSize { get; set; }

        public string TelephoneNo { get; set; }

        public bool ExcludeFromCover { get; set; }

        public virtual LocationModel Location { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Rooms.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
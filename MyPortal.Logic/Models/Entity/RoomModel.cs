using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class RoomModel : BaseModel
    {
        public Guid? LocationId { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public int MaxGroupSize { get; set; }

        public string TelephoneNo { get; set; }

        public bool ExcludeFromCover { get; set; }

        public virtual LocationModel Location { get; set; }

        public virtual ExamRoomModel ExamRoom { get; set; }
    }
}
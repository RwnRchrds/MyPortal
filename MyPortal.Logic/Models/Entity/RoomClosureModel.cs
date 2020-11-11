using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class RoomClosureModel : BaseModel
    {
        public Guid RoomId { get; set; }
        
        public Guid ReasonId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        [StringLength(256)]
        public string Notes { get; set; }

        public virtual RoomModel Room { get; set; }
        public virtual RoomClosureReasonModel Reason { get; set; }
    }
}
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class RoomClosureReasonModel : LookupItemModel
    {
        public RoomClosureReasonModel(RoomClosureReason model) : base(model)
        {
            System = model.System;
        }
        
        public bool System { get; set; }
    }
}
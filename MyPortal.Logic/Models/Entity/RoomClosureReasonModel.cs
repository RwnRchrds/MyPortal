using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class RoomClosureReasonModel : LookupItemModel
    {
        public bool System { get; set; }
        
        public bool Exam { get; set; }
    }
}
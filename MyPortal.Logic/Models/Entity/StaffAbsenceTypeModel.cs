using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StaffAbsenceTypeModel : LookupItemModel
    {
        public bool System { get; set; }
        
        public bool Illness { get; set; }
    }
}
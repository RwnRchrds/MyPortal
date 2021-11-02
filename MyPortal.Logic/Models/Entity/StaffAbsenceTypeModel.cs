using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StaffAbsenceTypeModel : LookupItemModel
    {
        public StaffAbsenceTypeModel(StaffAbsenceType model) : base(model)
        {
            System = model.System;
            Authorised = model.Authorised;
        }
        
        public bool System { get; set; }
        public bool Authorised { get; set; }
    }
}
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.StaffMembers
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
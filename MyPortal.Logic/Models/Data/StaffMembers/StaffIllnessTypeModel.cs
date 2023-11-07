using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.StaffMembers
{
    public class StaffIllnessTypeModel : LookupItemModel
    {
        public StaffIllnessTypeModel(StaffIllnessType model) : base(model)
        {
            System = model.System;
        }

        public bool System { get; set; }
    }
}
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class NextOfKinRelationshipTypeModel : LookupItemModel
    {
        public NextOfKinRelationshipTypeModel(NextOfKinRelationshipType model) : base(model)
        {
            System = model.System;
        }

        public bool System { get; set; }
    }
}
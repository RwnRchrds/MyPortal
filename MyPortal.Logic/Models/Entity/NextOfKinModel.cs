using System;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class NextOfKinModel : BaseModel
    {
        public NextOfKinModel(NextOfKin model) : base(model)
        {
            StaffMemberId = model.StaffMemberId;
            NextOfKinPersonId = model.NextOfKinPersonId;
            RelationshipTypeId = model.RelationshipTypeId;

            if (model.StaffMember != null)
            {
                StaffMember = new StaffMemberModel(model.StaffMember);
            }
            
            if (model.)
        }
        
        public Guid StaffMemberId { get; set; }
        public Guid NextOfKinPersonId { get; set; }
        public Guid RelationshipTypeId { get; set; }

        public virtual StaffMemberModel StaffMember { get; set; }
        public virtual PersonModel NextOfKinPerson { get; set; }
        public virtual NextOfKinRelationshipTypeModel RelationshipType { get; set; }
    }
}
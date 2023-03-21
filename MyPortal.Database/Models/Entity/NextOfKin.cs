using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("NextOfKin")]
    public class NextOfKin : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid StaffMemberId { get; set; }
        
        [Column(Order = 3)]
        public Guid NextOfKinPersonId { get; set; }
        
        [Column(Order = 4)]
        public Guid RelationshipTypeId { get; set; }

        public virtual StaffMember StaffMember { get; set; }
        public virtual Person NextOfKinPerson { get; set; }
        public virtual NextOfKinRelationshipType RelationshipType { get; set; }
    }
}
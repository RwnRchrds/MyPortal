using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentContactRelationshipModel : BaseModel
    {
        public Guid RelationshipTypeId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public Guid ContactId { get; set; }
        
        public bool Correspondence { get; set; }
        
        public bool ParentalResponsibility { get; set; }
        
        public bool PupilReport { get; set; }
        
        public bool CourtOrder { get; set; }

        public virtual ContactRelationshipTypeModel RelationshipType { get; set; }
        public virtual StudentModel Student { get; set; }
        public virtual ContactModel Contact { get; set; }
    }
}
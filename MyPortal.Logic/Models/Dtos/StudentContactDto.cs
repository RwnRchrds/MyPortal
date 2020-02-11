using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class StudentContactDto
    {
        public Guid Id { get; set; }
        public Guid RelationshipTypeId { get; set; }
        public Guid StudentId { get; set; }
        public Guid ContactId { get; set; }

        public bool Correspondence { get; set; }
        public bool ParentalResponsibility { get; set; }
        public bool PupilReport { get; set; }
        public bool CourtOrder { get; set; }

        public virtual RelationshipTypeDto RelationshipType { get; set; }
        public virtual StudentDto Student { get; set; }
        public virtual ContactDto Contact { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class StudentContactDto
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int StudentId { get; set; }
        public int ContactId { get; set; }

        public bool Correspondence { get; set; }
        public bool ParentalResponsibility { get; set; }
        public bool PupilReport { get; set; }
        public bool CourtOrder { get; set; }

        public virtual StudentDto Student { get; set; }
        public virtual ContactDto Contact { get; set; }
    }
}

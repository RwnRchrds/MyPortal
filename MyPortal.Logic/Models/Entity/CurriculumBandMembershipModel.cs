using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumBandMembershipModel
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid BandId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual CurriculumBandModel Band { get; set; }

        public virtual StudentModel Student { get; set; }
    }
}

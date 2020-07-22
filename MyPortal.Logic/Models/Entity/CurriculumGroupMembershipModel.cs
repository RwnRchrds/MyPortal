using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumGroupMembershipModel
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid GroupId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual CurriculumGroupModel CurriculumGroup { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AcademicTermModel : BaseModel
    {
        public Guid AcademicYearId { get; set; }

        [StringLength(128)]
        public string Name { get; set; }    

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }
    }
}

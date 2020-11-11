using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardModel : BaseModel
    {
        public Guid StudentId { get; set; }

        public Guid BehaviourTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public bool Active { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual IncidentTypeModel BehaviourType { get; set; }
    }
}

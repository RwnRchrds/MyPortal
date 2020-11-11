using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class PersonConditionModel : BaseModel
    {
        public Guid PersonId { get; set; }

        public Guid ConditionId { get; set; }

        public bool MedicationTaken { get; set; }

        [StringLength(256)]
        public string Medication { get; set; }

        public virtual PersonModel Person { get; set; }
        public virtual MedicalConditionModel MedicalCondition { get; set; }
    }
}

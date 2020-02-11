using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class PersonConditionDto
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid ConditionId { get; set; }

        public bool MedicationTaken { get; set; }

        [StringLength(256)]
        public string Medication { get; set; }

        public virtual PersonDto Person { get; set; }
        public virtual MedicalConditionDto MedicalCondition { get; set; }
    }
}

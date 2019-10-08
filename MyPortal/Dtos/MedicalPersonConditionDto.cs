using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Dtos
{
    
    public class MedicalPersonConditionDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int ConditionId { get; set; }

        public bool MedicationTaken { get; set; }

        public string Medication { get; set; }

        public virtual PersonDto Person { get; set; }
        public virtual MedicalConditionDto Condition { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("Medical_StudentConditions")]
    public class MedicalStudentCondition
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ConditionId { get; set; }

        public bool MedicationTaken { get; set; }

        [StringLength(100)]
        public string Medication { get; set; }

        public virtual MedicalCondition MedicalCondition { get; set; }

        public virtual Student Student { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("Medical_DietaryRequirements")]
    public class MedicalDietaryRequirements
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
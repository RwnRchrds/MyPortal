using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("Medical_DietaryRequirements")]
    public class MedicalDietaryRequirement
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public string Description { get; set; }
    }
}
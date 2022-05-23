using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateAcademicYearRequestModel
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public CreateAcademicTermRequestModel[] AcademicTerms { get; set; }
    }
}

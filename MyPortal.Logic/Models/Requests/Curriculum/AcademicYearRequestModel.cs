﻿using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class AcademicYearRequestModel
    {
        [Required] [StringLength(128)] public string Name { get; set; }

        public bool Locked { get; set; }

        public AcademicTermRequestModel[] AcademicTerms { get; set; }
    }
}
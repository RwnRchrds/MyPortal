using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class UpdateAcademicYearRequestModel
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public bool Locked { get; set; }
    }
}
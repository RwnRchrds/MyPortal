using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    public class SenGiftedTalented
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        [Required]
        public string Notes { get; set; }

        public virtual Student Student { get; set; }
        public virtual CurriculumSubject Subject { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class SenGiftedTalentedDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        
        public string Notes { get; set; }

        public virtual StudentDto Student { get; set; }
        public virtual CurriculumSubjectDto Subject { get; set; }
    }
}
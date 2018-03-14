using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class TrainingCourseDto
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Code { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }
}
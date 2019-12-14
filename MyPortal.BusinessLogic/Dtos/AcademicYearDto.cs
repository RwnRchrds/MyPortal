using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Models.Attributes;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AcademicYearDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        [AcademicYearLastDate]
        public DateTime LastDate { get; set; }
    }
}

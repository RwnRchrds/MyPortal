using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AttendanceCodeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public int MeaningId { get; set; }

        public bool DoNotUse { get; set; }

        public virtual AttendanceCodeMeaning CodeMeaning { get; set; }

        public string GetCodeMeaning()
        {
            return CodeMeaning.Description;
        }
    }
}

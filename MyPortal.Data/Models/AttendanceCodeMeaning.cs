using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("AttendanceCodeMeaning")]
    public class AttendanceCodeMeaning
    {
        public AttendanceCodeMeaning()
        {
            Codes = new HashSet<AttendanceCode>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<AttendanceCode> Codes { get; set; }
    }
}

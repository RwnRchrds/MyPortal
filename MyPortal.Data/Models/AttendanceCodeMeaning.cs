using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("CodeMeaning", Schema = "attendance")]
    public class AttendanceCodeMeaning
    {
        public AttendanceCodeMeaning()
        {
            Codes = new HashSet<AttendanceCode>();
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public virtual ICollection<AttendanceCode> Codes { get; set; }
    }
}

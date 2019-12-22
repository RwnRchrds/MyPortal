using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class ResultDto
    {
        public int Id { get; set; }

        public int ResultSetId { get; set; }

        public int StudentId { get; set; }

        public int AspectId { get; set; }
        public DateTime Date { get; set; }

        [Required]
        [StringLength(128)]
        public string Grade { get; set; }

        public virtual ResultSetDto ResultSet { get; set; }

        public virtual AspectDto Aspect { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}

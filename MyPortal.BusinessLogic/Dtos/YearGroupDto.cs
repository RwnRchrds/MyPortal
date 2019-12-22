using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class YearGroupDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int HeadId { get; set; }

        public int KeyStage { get; set; }

        public virtual StaffMemberDto HeadOfYear { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class CommentBankDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public bool System { get; set; }

        public bool InUse { get; set; }
    }
}

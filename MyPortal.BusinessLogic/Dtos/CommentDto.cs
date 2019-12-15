using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int CommentBankId { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual CommentBankDto CommentBank { get; set; }
    }
}

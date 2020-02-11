using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public Guid CommentBankId { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual CommentBankDto CommentBank { get; set; }
    }
}

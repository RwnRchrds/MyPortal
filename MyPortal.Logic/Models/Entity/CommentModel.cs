using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CommentModel : BaseModel
    {
        public Guid CommentBankId { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual CommentBankModel CommentBank { get; set; }
    }
}

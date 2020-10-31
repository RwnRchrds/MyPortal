using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CommentBankModel : BaseModel
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.BaseTypes
{
    public abstract class Audit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedById { get; set; }
    }
}

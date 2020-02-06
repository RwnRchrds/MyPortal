using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("HomeworkAttachment")]
    public class HomeworkAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid HomeworkId { get; set; }
        public Guid DocumentId { get; set; }

        public virtual Homework Homework { get; set; }
        public virtual Document Document { get; set; }
    }
}

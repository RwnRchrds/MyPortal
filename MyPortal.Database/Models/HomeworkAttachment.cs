using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("HomeworkAttachment")]
    public class HomeworkAttachment
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public int DocumentId { get; set; }

        public virtual Homework Homework { get; set; }
        public virtual Document Document { get; set; }
    }
}

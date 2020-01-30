using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("Homework")]
    public class Homework
    {
        public Homework()
        {
            Submissions = new HashSet<HomeworkSubmission>();
            Attachments = new HashSet<HomeworkAttachment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<HomeworkSubmission> Submissions { get; set; }
        public virtual ICollection<HomeworkAttachment> Attachments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("HomeworkItems")]
    public class HomeworkItem : BaseTypes.Entity
    {
        public HomeworkItem()
        {
            Submissions = new HashSet<HomeworkSubmission>();
        }

        [Column(Order = 1)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 2)]
        public string Title { get; set; }

        [Column(Order = 3)]
        public string Description { get; set; }

        [Column(Order = 4)]
        public bool SubmitOnline { get; set; }

        public virtual ICollection<HomeworkSubmission> Submissions { get; set; }
        public virtual Directory Directory { get; set; }
    }
}

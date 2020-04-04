using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Homework")]
    public class Homework : IDirectoryEntity
    {
        public Homework()
        {
            Submissions = new HashSet<HomeworkSubmission>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid DirectoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool SubmitOnline { get; set; }

        public virtual ICollection<HomeworkSubmission> Submissions { get; set; }
        public virtual Directory Directory { get; set; }
    }
}

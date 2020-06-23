using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
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

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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

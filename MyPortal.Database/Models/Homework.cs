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

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid DirectoryId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool SubmitOnline { get; set; }

        public virtual ICollection<HomeworkSubmission> Submissions { get; set; }
        public virtual Directory Directory { get; set; }
    }
}

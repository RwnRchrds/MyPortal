using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("Directory")]
    public class Directory
    {
        public Directory()
        {
            Subdirectories = new HashSet<Directory>();
            Documents = new HashSet<Document>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [DataMember]
        public Guid? ParentId { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [DataMember]
        public bool Private { get; set; }

        [DataMember]
        public bool StaffOnly { get; set; } 

        public virtual Directory Parent { get; set; }
        public virtual Bulletin Bulletin { get; set; }
        public virtual Homework Homework { get; set; }
        public virtual Person Person { get; set; }
        public virtual LessonPlan LessonPlan { get; set; }

        public virtual ICollection<Directory> Subdirectories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}

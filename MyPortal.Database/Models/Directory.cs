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

        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid? ParentId { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 3)]
        public bool Private { get; set; }

        [Column(Order = 4)]
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

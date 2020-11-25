using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.Attributes;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Directories")]
    public class Directory : Entity
    {
        public Directory()
        {
            Subdirectories = new HashSet<Directory>();
            Documents = new HashSet<Document>();
        }

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

        [EntityOnly]
        public virtual Bulletin Bulletin { get; set; }

        [EntityOnly]
        public virtual HomeworkItem HomeworkItem { get; set; }

        [EntityOnly]
        public virtual Person Person { get; set; }

        [EntityOnly]
        public virtual LessonPlan LessonPlan { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual ICollection<Directory> Subdirectories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}

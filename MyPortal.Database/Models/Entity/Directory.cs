using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Directories")]
    public class Directory : BaseTypes.Entity
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

        // Belongs to a Person
        [Column(Order = 3)]
        public bool Private { get; set; }

        // Only visible to staff users
        [Column(Order = 4)]
        public bool Restricted { get; set; } 

        public virtual Directory Parent { get; set; }
        public virtual ICollection<Agency> Agencies { get; set; }
        public virtual ICollection<Bulletin> Bulletins { get; set; }
        public virtual ICollection<HomeworkItem> HomeworkItems { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
        public virtual ICollection<Directory> Subdirectories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Classes")]
    public class Class : BaseTypes.Entity, IDirectoryEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            Sessions = new HashSet<Session>();
        }

        [Column(Order = 2)] public Guid CourseId { get; set; }

        [Column(Order = 3)] public Guid CurriculumGroupId { get; set; }

        [Column(Order = 4)] public Guid DirectoryId { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public virtual Course Course { get; set; }
        public virtual CurriculumGroup Group { get; set; }
        public virtual Directory Directory { get; set; }


        public virtual ICollection<Session> Sessions { get; set; }
    }
}
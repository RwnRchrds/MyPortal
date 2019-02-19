using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    [Table("Pastoral_YearGroups")]
    public class YearGroup
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YearGroup()
        {
            RegGroups = new HashSet<RegGroup>();
            Students = new HashSet<Student>();
            StudyTopics = new HashSet<StudyTopic>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required] [StringLength(255)] public string Name { get; set; }

        [Display(Name = "Head of Year")] public int HeadId { get; set; }

        [Display(Name = "Key Stage")] public int KeyStage { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegGroup> RegGroups { get; set; }

        public virtual Staff Staff { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
        
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudyTopic> StudyTopics { get; set; }
    }
}
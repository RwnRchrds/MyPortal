using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("LessonPlanTemplate")]
    public class LessonPlanTemplate
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public string PlanTemplate { get; set; }
    }
}

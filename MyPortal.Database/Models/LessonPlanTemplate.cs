using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("LessonPlanTemplate")]
    public class LessonPlanTemplate : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 2)]
        [Required]
        public string PlanTemplate { get; set; }
    }
}

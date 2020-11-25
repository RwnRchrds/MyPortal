using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("GiftedTalentedStudents")]
    public class GiftedTalented : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid SubjectId { get; set; }

        [Column(Order = 3)]
        [Required]
        public string Notes { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
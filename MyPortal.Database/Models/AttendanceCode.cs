using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("AttendanceCode")]
    public partial class AttendanceCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public Guid MeaningId { get; set; }

        public bool DoNotUse { get; set; }

        public virtual AttendanceCodeMeaning CodeMeaning { get; set; }
    }
}

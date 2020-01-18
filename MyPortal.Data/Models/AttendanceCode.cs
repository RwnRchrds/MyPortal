using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// [READ-ONLY] Codes available to use when taking the register.
    /// </summary>
    [Table("AttendanceCode")]
    public partial class AttendanceCode
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS
        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public int MeaningId { get; set; }

        public bool DoNotUse { get; set; }

        public virtual AttendanceCodeMeaning CodeMeaning { get; set; }
    }
}

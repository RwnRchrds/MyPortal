using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Dtos
{
    public class AttendanceCodeDto
    {
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

        public string GetCodeMeaning()
        {
            return CodeMeaning.Description;
        }
    }
}

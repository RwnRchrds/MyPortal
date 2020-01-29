using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Dtos
{
    public class GiftedTalentedDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        [Required]
        public string Notes { get; set; }

        public virtual StudentDto Student { get; set; }
        public virtual SubjectDto Subject { get; set; }
    }
}

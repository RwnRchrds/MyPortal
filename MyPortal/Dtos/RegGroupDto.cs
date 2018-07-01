using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Dtos
{
    public class RegGroupDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required] [StringLength(3)] public string Name { get; set; }

        [Required] [StringLength(3)] public string Tutor { get; set; }

        [Required] public int YearGroup { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Dtos
{
    public class YearGroupDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required] [StringLength(255)] public string Name { get; set; }

        [Required] [StringLength(3)] public int HeadId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Dtos
{
    public class AspectDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int GradeSetId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual AspectTypeDto Type { get; set; }

        public virtual GradeSetDto GradeSet { get; set; }
    }
}

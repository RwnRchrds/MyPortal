using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Report")]
    public class Report
    {
        public int Id { get; set; }

        public int AreaId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool Restricted { get; set; }

        public virtual SystemArea SystemArea { get; set; }
    }
}
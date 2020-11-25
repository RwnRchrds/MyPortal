using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Reports")]
    public class Report : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid AreaId { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [Column(Order = 4)]
        public bool Restricted { get; set; }

        public virtual SystemArea SystemArea { get; set; }
    }
}
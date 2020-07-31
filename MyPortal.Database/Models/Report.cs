using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Reports")]
    public class Report : Entity
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
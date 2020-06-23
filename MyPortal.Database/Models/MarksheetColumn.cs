using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("MarksheetColumn")]
    public class MarksheetColumn
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid TemplateId { get; set; }

        [Column(Order = 2)]
        public Guid AspectId { get; set; }

        [Column(Order = 3)]
        public Guid ResultSetId { get; set; }

        [Column(Order = 4)]
        public int ColumnOrder { get; set; }

        public virtual MarksheetTemplate Template { get; set; }
        public virtual Aspect Aspect { get; set; }
        public virtual ResultSet ResultSet { get; set; }
    }
}
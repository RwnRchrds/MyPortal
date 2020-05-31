using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("MarksheetColumn")]
    public class MarksheetColumn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public Guid AspectId { get; set; }
        public Guid ResultSetId { get; set; }
        public int ColumnOrder { get; set; }

        public virtual MarksheetTemplate Template { get; set; }
        public virtual Aspect Aspect { get; set; }
        public virtual ResultSet ResultSet { get; set; }
    }
}
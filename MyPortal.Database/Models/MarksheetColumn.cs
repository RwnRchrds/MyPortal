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
        public Guid MarksheetId { get; set; }
        public Guid AspectId { get; set; }
        public Guid ResultSetId { get; set; }

        public virtual Marksheet Marksheet { get; set; }
        public virtual Aspect Aspect { get; set; }
        public virtual ResultSet ResultSet { get; set; }
    }
}
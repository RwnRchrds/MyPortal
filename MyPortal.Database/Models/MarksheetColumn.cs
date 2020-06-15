using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("MarksheetColumn")]
    public class MarksheetColumn
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TemplateId { get; set; }

        [DataMember]
        public Guid AspectId { get; set; }

        [DataMember]
        public Guid ResultSetId { get; set; }

        [DataMember]
        public int ColumnOrder { get; set; }

        public virtual MarksheetTemplate Template { get; set; }
        public virtual Aspect Aspect { get; set; }
        public virtual ResultSet ResultSet { get; set; }
    }
}
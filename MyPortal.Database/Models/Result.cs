using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Result")]
    public class Result
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ResultSetId { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid AspectId { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [DataMember]
        public Guid? GradeId { get; set; }

        [DataMember]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Mark { get; set; }

        [DataMember]
        public string Comments { get; set; }

        public virtual ResultSet ResultSet { get; set; }

        public virtual Aspect Aspect { get; set; }

        public virtual Student Student { get; set; }

        public virtual Grade Grade { get; set; }
    }
}

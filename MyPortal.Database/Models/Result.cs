using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Result")]
    public class Result
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ResultSetId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AspectId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public Guid? GradeId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Mark { get; set; }

        public string Comments { get; set; }

        public virtual ResultSet ResultSet { get; set; }

        public virtual Aspect Aspect { get; set; }

        public virtual Student Student { get; set; }

        public virtual Grade Grade { get; set; }
    }
}

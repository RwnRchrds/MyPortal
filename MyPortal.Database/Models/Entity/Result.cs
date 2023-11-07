using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Results")]
    public class Result : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid ResultSetId { get; set; }

        [Column(Order = 3)] public Guid StudentId { get; set; }

        [Column(Order = 4)] public Guid AspectId { get; set; }

        [Column(Order = 5)] public Guid CreatedById { get; set; }

        [Column(Order = 6, TypeName = "date")] public DateTime Date { get; set; }

        [Column(Order = 7)] public Guid? GradeId { get; set; }

        [Column(Order = 8, TypeName = "decimal(10,2)")]
        public decimal? Mark { get; set; }

        // Used for comment result types
        [Column(Order = 9)]
        [StringLength(1000)]
        public string Comment { get; set; }

        [Column(Order = 10)] public string ColourCode { get; set; }

        // Used to add notes/comments to results
        [Column(Order = 11)] public string Note { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ResultSet ResultSet { get; set; }

        public virtual Aspect Aspect { get; set; }

        public virtual Student Student { get; set; }

        public virtual Grade Grade { get; set; }
    }
}
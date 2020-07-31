using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Results")]
    public class Result : Entity
    {
        [Column(Order = 1)]
        public Guid ResultSetId { get; set; }

        [Column(Order = 2)]
        public Guid StudentId { get; set; }

        [Column(Order = 3)]
        public Guid AspectId { get; set; }

        [Column(Order = 4, TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(Order = 5)]
        public Guid? GradeId { get; set; }

        [Column(Order = 6, TypeName = "decimal(10,2)")]
        public decimal? Mark { get; set; }

        [Column(Order = 7)]
        public string Comments { get; set; }

        public virtual ResultSet ResultSet { get; set; }

        public virtual Aspect Aspect { get; set; }

        public virtual Student Student { get; set; }

        public virtual Grade Grade { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("SenReview")]
    public class SenReview
    {
        [Column(Order = 0)]
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid ReviewTypeId { get; set; }

        [Column(Order = 3)]
        public DateTime Date { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Outcome { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenReviewType ReviewType { get; set; }
    }
}
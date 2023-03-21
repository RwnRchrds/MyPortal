using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("SenReviews")]
    public class SenReview : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid StudentId { get; set; }

        [Column(Order = 3)]
        public Guid ReviewTypeId { get; set; }

        [Column(Order = 4)]
        public DateTime Date { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 6)]
        [StringLength(256)]
        public string Outcome { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenReviewType ReviewType { get; set; }
    }
}
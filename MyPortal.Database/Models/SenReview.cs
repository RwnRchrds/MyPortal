using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("SenReview")]
    public class SenReview
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid ReviewTypeId { get; set; }
        
        public DateTime Date { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string Outcome { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenReviewType ReviewType { get; set; }
    }
}
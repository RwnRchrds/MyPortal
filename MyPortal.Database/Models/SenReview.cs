using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("SenReview")]
    public class SenReview
    {
        [DataMember]
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid ReviewTypeId { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Description { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Outcome { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenReviewType ReviewType { get; set; }
    }
}
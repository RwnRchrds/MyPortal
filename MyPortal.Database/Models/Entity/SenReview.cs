using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("SenReviews")]
    public class SenReview : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid ReviewTypeId { get; set; }

        [Column(Order = 4)] public Guid ReviewStatusId { get; set; }

        [Column(Order = 5)] public Guid? SencoId { get; set; }

        [Column(Order = 6)] public Guid EventId { get; set; }

        [Column(Order = 7)]
        // When this gets updated, the student's SEN status should also be updated
        public Guid? OutcomeSenStatusId { get; set; }

        [Column(Order = 8)]
        [StringLength(256)]
        public string Comments { get; set; }

        public virtual Student Student { get; set; }

        public virtual StaffMember Senco { get; set; }

        public virtual DiaryEvent Event { get; set; }

        public virtual SenStatus OutcomeStatus { get; set; }

        public virtual SenReviewStatus ReviewStatus { get; set; }

        public virtual SenReviewType ReviewType { get; set; }
    }
}
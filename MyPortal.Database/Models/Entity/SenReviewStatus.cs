using System.Collections.Generic;

namespace MyPortal.Database.Models.Entity;

public class SenReviewStatus : BaseTypes.LookupItem
{
    public virtual ICollection<SenReview> SenReviews { get; set; }
}
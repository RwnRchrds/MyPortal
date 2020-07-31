using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("SenReviewTypes")]
    public class SenReviewType : LookupItem
    {
        public virtual ICollection<SenReview> Reviews { get; set; }
    }
}
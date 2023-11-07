using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Exclusions")]
    public class Exclusion : BaseTypes.Entity, ISoftDeleteEntity
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid ExclusionTypeId { get; set; }

        [Column(Order = 4)] public Guid ExclusionReasonId { get; set; }

        [Column(Order = 5, TypeName = "date")] public DateTime StartDate { get; set; }

        [Column(Order = 6, TypeName = "date")] public DateTime? EndDate { get; set; }

        [Column(Order = 7)] public string Comments { get; set; }

        [Column(Order = 8)] public bool Deleted { get; set; }

        #region Appeal

        [Column(Order = 9, TypeName = "date")] public DateTime? AppealDate { get; set; }

        [Column(Order = 10, TypeName = "date")]
        public DateTime? AppealResultDate { get; set; }

        [Column(Order = 11)] public Guid? AppealResultId { get; set; }

        #endregion

        public virtual Student Student { get; set; }
        public virtual ExclusionType ExclusionType { get; set; }
        public virtual ExclusionReason ExclusionReason { get; set; }
        public virtual ExclusionAppealResult AppealResult { get; set; }
    }
}
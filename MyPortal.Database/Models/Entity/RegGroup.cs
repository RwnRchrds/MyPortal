using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("RegGroups")]
    public class RegGroup : BaseTypes.Entity, IStudentGroupEntity
    {
        [Column(Order = 2)] public Guid StudentGroupId { get; set; }

        [Column(Order = 3)] public Guid YearGroupId { get; set; }

        [Column(Order = 4)] public Guid? RoomId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual YearGroup YearGroup { get; set; }
    }
}
using System;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Models.Query.Student
{
    /// <summary>
    /// Student entity with pastoral groups.
    /// </summary>
    public class ExtendedStudent : Entity.Student
    {
        public Guid HouseId { get; set; }
        public Guid RegGroupId { get; set; }
        public Guid YearGroupId { get; set; }

        public House House { get; set; }
        public RegGroup RegGroup { get; set; }
        public YearGroup YearGroup { get; set; }
    }
}
using System;

namespace MyPortal.Logic.Models.Admissions
{
    public class ApplicantAdmission
    {
        public Guid ApplicantId { get; set; }
        public Guid RegGroupId { get; set; }
        public Guid HouseId { get; set; }
    }
}
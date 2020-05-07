using System;

namespace MyPortal.Logic.Models.Requests.Admissions
{
    public class ApplicantAdmission
    {
        public Guid ApplicantId { get; set; }
        public Guid RegGroupId { get; set; }
        public Guid HouseId { get; set; }
    }
}
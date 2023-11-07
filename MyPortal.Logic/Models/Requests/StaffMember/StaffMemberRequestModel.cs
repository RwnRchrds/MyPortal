using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Models.Requests.StaffMember
{
    public class StaffMemberRequestModel : PersonRequestModel
    {
        public Guid? LineManagerId { get; set; }

        public Guid PersonId { get; set; }

        [Required] [StringLength(128)] public string Code { get; set; }

        [StringLength(50)] public string BankName { get; set; }

        [StringLength(15)] public string BankAccount { get; set; }

        [StringLength(10)] public string BankSortCode { get; set; }

        [StringLength(9)] public string NiNumber { get; set; }

        [StringLength(128)] public string Qualifications { get; set; }

        public bool TeachingStaff { get; set; }

        public bool Deleted { get; set; }
    }
}
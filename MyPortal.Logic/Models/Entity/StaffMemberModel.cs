using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class StaffMemberModel : BaseModel
    {
        public StaffMemberModel(StaffMember staffMember)
        {
            Id = staffMember.Id;
            LineManagerId = staffMember.LineManagerId;
            PersonId = staffMember.PersonId;
            Code = staffMember.Code;
            BankName = staffMember.BankName;
            BankAccount = staffMember.BankAccount;
            BankSortCode = staffMember.BankSortCode;
            NiNumber = staffMember.NiNumber;
            Qualifications = staffMember.Qualifications;
            TeachingStaff = staffMember.TeachingStaff;
            Deleted = staffMember.Deleted;
        }
        
        public Guid? LineManagerId { get; set; }
        
        public Guid PersonId { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Code { get; set; }
        
        [StringLength(50)]
        public string BankName { get; set; }
        
        [StringLength(15)]
        public string BankAccount { get; set; }
        
        [StringLength(10)]
        public string BankSortCode { get; set; }
        
        [StringLength(9)]
        public string NiNumber { get; set; }
        
        [StringLength(128)]
        public string Qualifications { get; set; }
        
        public bool TeachingStaff { get; set; }
        
        public bool Deleted { get; set; }

        public virtual PersonModel Person { get; set; }

        public virtual NextOfKinModel NextOfKin { get; set; }

        public virtual StaffMemberModel LineManager { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class RegGroupModel
    {
        public RegGroupModel(RegGroup regGroup)
        {
            Id = regGroup.Id;
            StudentGroupId = regGroup.StudentGroupId;
            YearGroupId = regGroup.YearGroupId;
        }
        
        public Guid Id { get; set; }

        public Guid StudentGroupId { get; set; }
        
        public Guid YearGroupId { get; set; }

        public virtual StaffMemberModel Tutor { get; set; }

        public virtual YearGroupModel YearGroup { get; set; }
    }
}
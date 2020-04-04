using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Business
{
    public class YearGroupModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid? HeadId { get; set; }

        public virtual StaffMemberModel HeadOfYear { get; set; }
    }
}
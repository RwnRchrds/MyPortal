using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.Models;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Dtos
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid RegGroupId { get; set; }

        public Guid YearGroupId { get; set; }

        public Guid? HouseId { get; set; }

        [StringLength(128)]
        public string CandidateNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdmissionNumber { get; set; }

        public DateTime? DateStarting { get; set; }

        public DateTime? DateLeaving { get; set; }

        public decimal AccountBalance { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public bool GiftedAndTalented { get; set; }

        public int? SenStatusId { get; set; }

        public bool PupilPremium { get; set; }

        [Upn]
        [StringLength(13)]
        public string Upn { get; set; }

        public string Uci { get; set; }

        public bool Deleted { get; set; }

        public virtual RegGroupDto RegGroup { get; set; }

        public virtual YearGroupDto YearGroup { get; set; }

        public virtual PersonDto Person { get; set; }

        public virtual SenStatusDto SenStatus { get; set; }

        public virtual HouseDto House { get; set; }
    }
}

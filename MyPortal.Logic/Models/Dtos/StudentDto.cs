using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Logic.Models.Dtos
{
    public class StudentDto : IPersonDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int RegGroupId { get; set; }

        public int YearGroupId { get; set; }

        public int? HouseId { get; set; }

        [StringLength(128)]
        public string CandidateNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdmissionNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateStarting { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateLeaving { get; set; }

        public decimal AccountBalance { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public bool GiftedAndTalented { get; set; }

        public int? SenStatusId { get; set; }

        public bool PupilPremium { get; set; }

        [StringLength(256)]
        public string MisId { get; set; }

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

using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.DataGrid;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentModel : BaseModel
    {
        public Guid PersonId { get; set; }

        public Guid RegGroupId { get; set; }

        public Guid YearGroupId { get; set; }

        public Guid? HouseId { get; set; }

        public int AdmissionNumber { get; set; }

        public DateTime? DateStarting { get; set; }

        public DateTime? DateLeaving { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public Guid? SenStatusId { get; set; }

        public bool PupilPremium { get; set; }

        public string Upn { get; set; }

        public bool Deleted { get; set; }

        public virtual RegGroupModel RegGroup { get; set; }

        public virtual YearGroupModel YearGroup { get; set; }

        public virtual PersonModel Person { get; set; }

        public virtual ExamCandidateModel Candidate { get; set; }

        public virtual SenStatusModel SenStatus { get; set; }

        public virtual HouseModel House { get; set; }

        public StudentDataGridModel GetDataGridModel()
        {
            return new StudentDataGridModel(this);
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentModel : BaseModel
    {
        public StudentModel(Student student)
        {
            Id = student.Id;
            PersonId = student.PersonId;
            AdmissionNumber = student.AdmissionNumber;
            DateStarting = student.DateStarting;
            DateLeaving = student.DateLeaving;
            FreeSchoolMeals = student.FreeSchoolMeals;
            SenStatusId = student.SenStatusId;
            SenTypeId = student.SenTypeId;
            EnrolmentStatusId = student.EnrolmentStatusId;
            BoarderStatusId = student.BoarderStatusId;
            PupilPremium = student.PupilPremium;
            Upn = student.Upn;
            Deleted = student.Deleted;
        }
        
        public Guid PersonId { get; set; }


        public int AdmissionNumber { get; set; }

        
        public DateTime? DateStarting { get; set; }

        
        public DateTime? DateLeaving { get; set; }

        
        public bool FreeSchoolMeals { get; set; }

        
        public Guid? SenStatusId { get; set; }

        
        public Guid? SenTypeId { get; set; }

        
        public Guid? EnrolmentStatusId { get; set; }

        
        public Guid? BoarderStatusId { get; set; }

        
        public bool PupilPremium { get; set; }

        
        [StringLength(13)]
        public string Upn { get; set; }

        
        public bool Deleted { get; set; }
        
        public virtual PersonModel Person { get; set; }

        public virtual SenStatusModel SenStatus { get; set; }

        public virtual SenTypeModel SenType { get; set; }

        public virtual EnrolmentStatusModel EnrolmentStatus { get; set; }

        public virtual BoarderStatusModel BoarderStatus { get; set; }
    }
}
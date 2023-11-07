using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Data.Students.SEND;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Students
{
    public class StudentModel : BaseModelWithLoad
    {
        public StudentModel(Student model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Student model)
        {
            Id = model.Id;
            PersonId = model.PersonId;
            AdmissionNumber = model.AdmissionNumber;
            DateStarting = model.DateStarting;
            DateLeaving = model.DateLeaving;
            FreeSchoolMeals = model.FreeSchoolMeals;
            SenStatusId = model.SenStatusId;
            SenTypeId = model.SenTypeId;
            EnrolmentStatusId = model.EnrolmentStatusId;
            BoarderStatusId = model.BoarderStatusId;
            PupilPremium = model.PupilPremium;
            Upn = model.Upn;
            Deleted = model.Deleted;

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }

            if (model.SenStatus != null)
            {
                SenStatus = new SenStatusModel(model.SenStatus);
            }

            if (model.SenType != null)
            {
                SenType = new SenTypeModel(model.SenType);
            }

            if (model.EnrolmentStatus != null)
            {
                EnrolmentStatus = new EnrolmentStatusModel(model.EnrolmentStatus);
            }

            if (model.BoarderStatus != null)
            {
                BoarderStatus = new BoarderStatusModel(model.BoarderStatus);
            }
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


        [StringLength(13)] public string Upn { get; set; }


        public bool Deleted { get; set; }

        public virtual PersonModel Person { get; set; }

        public virtual SenStatusModel SenStatus { get; set; }

        public virtual SenTypeModel SenType { get; set; }

        public virtual EnrolmentStatusModel EnrolmentStatus { get; set; }

        public virtual BoarderStatusModel BoarderStatus { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Students.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
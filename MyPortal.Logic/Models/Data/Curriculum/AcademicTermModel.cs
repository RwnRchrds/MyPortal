using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class AcademicTermModel : BaseModelWithLoad
    {
        internal AcademicTermModel(AcademicTerm model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AcademicTerm model)
        {
            AcademicYearId = model.AcademicYearId;
            Name = model.Name;
            StartDate = model.StartDate;
            EndDate = model.EndDate;

            if (model.AcademicYear != null)
            {
                AcademicYear = new AcademicYearModel(model.AcademicYear);
            }
        }

        public Guid AcademicYearId { get; set; }

        [StringLength(128)] public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AcademicYearModel AcademicYear { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var term = await unitOfWork.AcademicTerms.GetById(Id.Value);

                if (term != null)
                {
                    LoadFromModel(term);
                }
            }
        }
    }
}
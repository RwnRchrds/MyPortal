using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AcademicTermModel : BaseModel, ILoadable
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

        [StringLength(128)]
        public string Name { get; set; }    

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AcademicYearModel AcademicYear { get; set; }
        public async System.Threading.Tasks.Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.AcademicTerms.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

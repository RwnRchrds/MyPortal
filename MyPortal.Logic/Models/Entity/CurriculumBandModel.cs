using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumBandModel : BaseModel, ILoadable
    {
        public CurriculumBandModel(CurriculumBand model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(CurriculumBand model)
        {
            AcademicYearId = model.AcademicYearId;
            CurriculumYearGroupId = model.CurriculumYearGroupId;
            StudentGroupId = model.StudentGroupId;

            if (model.AcademicYear != null)
            {
                AcademicYear = new AcademicYearModel(model.AcademicYear);
            }

            if (model.CurriculumYearGroup != null)
            {
                CurriculumYearGroup = new CurriculumYearGroupModel(model.CurriculumYearGroup);
            }

            if (model.StudentGroup != null)
            {
                StudentGroup = new StudentGroupModel(model.StudentGroup);
            }
        }
        
        public Guid AcademicYearId { get; set; }
        
        public Guid CurriculumYearGroupId { get; set; }
        
        public Guid StudentGroupId { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public AcademicYearModel AcademicYear { get; set; }
        public CurriculumYearGroupModel CurriculumYearGroup { get; set; }
        
        public StudentGroupModel StudentGroup { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.CurriculumBands.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
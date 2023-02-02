using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Students
{
    public class LogNoteModel : BaseModelWithLoad
    {
        public LogNoteModel(LogNote model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(LogNote model)
        {
            TypeId = model.TypeId;
            CreatedById = model.CreatedById;
            StudentId = model.StudentId;
            AcademicYearId = model.AcademicYearId;
            Message = model.Message;
            CreatedDate = model.CreatedDate;
            Private = model.Private;
            Deleted = model.Deleted;

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.AcademicYear != null)
            {
                AcademicYear = new AcademicYearModel(model.AcademicYear);
            }

            if (model.LogNoteType != null)
            {
                LogNoteType = new LogNoteTypeModel(model.LogNoteType);
            }
        }

        [NotEmpty]
        public Guid TypeId { get; set; }

        public Guid CreatedById { get; set; }

        [NotEmpty]
        public Guid StudentId { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Private { get; set; }

        public bool Deleted { get; set; }

        public virtual UserModel CreatedBy { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual LogNoteTypeModel LogNoteType { get; set; }

        public LogNoteSummaryModel ToListModel()
        {
            return new LogNoteSummaryModel(this);
        }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.LogNotes.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

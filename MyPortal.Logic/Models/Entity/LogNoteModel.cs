using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Collection;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class LogNoteModel : BaseModel, ILoadable
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

        public bool Deleted { get; set; }

        public virtual UserModel CreatedBy { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual LogNoteTypeModel LogNoteType { get; set; }

        public LogNoteListModel ToListModel()
        {
            return new LogNoteListModel(this);
        }

        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.LogNotes.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

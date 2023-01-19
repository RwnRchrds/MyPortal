using System;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ResultModel : BaseModelWithLoad
    {
        public ResultModel(Result model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Result model)
        {
            ResultSetId = model.ResultSetId;
            StudentId = model.StudentId;
            AspectId = model.AspectId;
            CreatedById = model.CreatedById;
            Date = model.Date;
            GradeId = model.GradeId;
            Mark = model.Mark;
            Comment = model.Comment;
            Note = model.Note;
            ColourCode = model.ColourCode;

            if (model.ResultSet != null)
            {
                ResultSet = new ResultSetModel(model.ResultSet);
            }

            if (model.Aspect != null)
            {
                Aspect = new AspectModel(model.Aspect);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Grade != null)
            {
                Grade = new GradeModel(model.Grade);
            }

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }
        }
        
        public Guid ResultSetId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AspectId { get; set; }
        
        public Guid CreatedById { get; set; }

        public DateTime Date { get; set; }

        public Guid? GradeId { get; set; }

        public decimal? Mark { get; set; }

        public string Comment { get; set; }

        public string Note { get; set; }

        public string ColourCode { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }

        public virtual AspectModel Aspect { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual GradeModel Grade { get; set; }
        
        public virtual UserModel CreatedBy { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Results.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

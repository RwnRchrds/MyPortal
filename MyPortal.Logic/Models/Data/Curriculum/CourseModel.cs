using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class CourseModel : LookupItemModelWithLoad
    {
        public CourseModel(Course model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Course model)
        {
            SubjectId = model.SubjectId;
            Name = model.Name;

            if (model.Subject != null)
            {
                Subject = new SubjectModel(model.Subject);
            }
        }

        public Guid SubjectId { get; set; }

        public string Name { get; set; }

        public virtual SubjectModel Subject { get; set; }


        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var course = await unitOfWork.Courses.GetById(Id.Value);

                if (course != null)
                {
                    LoadFromModel(course);
                }
            }
        }
    }
}
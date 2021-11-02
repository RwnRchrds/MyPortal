using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class TrainingCertificateModel : BaseModel, ILoadable
    {
        public TrainingCertificateModel(TrainingCertificate model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(TrainingCertificate model)
        {
            CourseId = model.CourseId;
            StaffId = model.StaffId;
            StatusId = model.StatusId;

            if (model.StaffMember != null)
            {
                StaffMember = new StaffMemberModel(model.StaffMember);
            }

            if (model.TrainingCourse != null)
            {
                TrainingCourse = new TrainingCourseModel(model.TrainingCourse);
            }

            if (model.Status != null)
            {
                Status = new TrainingCertificateStatusModel(model.Status);
            }
        }
        
        public Guid CourseId { get; set; }
        
        public Guid StaffId { get; set; }
        
        public Guid StatusId { get; set; }

        public virtual StaffMemberModel StaffMember { get; set; }

        public virtual TrainingCourseModel TrainingCourse { get; set; }

        public virtual TrainingCertificateStatusModel Status { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.TrainingCertificates.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
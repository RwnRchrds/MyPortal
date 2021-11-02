using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class SenReviewModel : BaseModel, ILoadable
    {
        public SenReviewModel(SenReview model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(SenReview model)
        {
            StudentId = model.StudentId;
            ReviewTypeId = model.ReviewTypeId;
            Date = model.Date;
            Description = model.Description;
            Outcome = model.Outcome;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.ReviewType != null)
            {
                ReviewType = new SenReviewTypeModel(model.ReviewType);
            }
        }
        
        public Guid StudentId { get; set; }
        
        public Guid ReviewTypeId { get; set; }
        
        public DateTime Date { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }
        
        [StringLength(256)]
        public string Outcome { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual SenReviewTypeModel ReviewType { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.SenReviews.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
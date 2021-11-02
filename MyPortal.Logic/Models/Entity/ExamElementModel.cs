using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamElementModel : BaseModel, ILoadable
    {
        public ExamElementModel(ExamElement model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamElement model)
        {
            BaseElementId = model.BaseElementId;
            SeriesId = model.SeriesId;
            Description = model.Description;
            ExamFee = model.ExamFee;
            Submitted = model.Submitted;

            if (model.BaseElement != null)
            {
                BaseElement = new ExamBaseElementModel(model.BaseElement);
            }

            if (model.Series != null)
            {
                Series = new ExamSeriesModel(model.Series);
            }
        }
        
        public Guid BaseElementId { get; set; }
        
        public Guid SeriesId { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }
        
        public decimal? ExamFee { get; set; }

        public bool Submitted { get; set; }

        public virtual ExamBaseElementModel BaseElement { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamElements.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
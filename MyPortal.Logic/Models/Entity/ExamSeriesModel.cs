using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSeriesModel : BaseModelWithLoad
    {
        public ExamSeriesModel(ExamSeries model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamSeries model)
        {
            ExamBoardId = model.ExamBoardId;
            ExamSeasonId = model.ExamSeasonId;
            SeriesCode = model.SeriesCode;
            Code = model.Code;
            Title = model.Title;

            if (model.Season != null)
            {
                Season = new ExamSeasonModel(model.Season);
            }

            if (model.ExamBoard != null)
            {
                ExamBoard = new ExamBoardModel(model.ExamBoard);
            }
        }
        
        public Guid ExamBoardId { get; set; }
        
        public Guid ExamSeasonId { get; set; }
        
        public string SeriesCode { get; set; }
        
        public string Code { get; set; }
        
        public string Title { get; set; }

        public virtual ExamSeasonModel Season { get; set; }
        public virtual ExamBoardModel ExamBoard { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamSeries.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
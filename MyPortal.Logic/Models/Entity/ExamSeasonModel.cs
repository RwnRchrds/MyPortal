using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSeasonModel : BaseModel, ILoadable
    {
        public ExamSeasonModel(ExamSeason model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamSeason model)
        {
            ResultSetId = model.ResultSetId;
            CalendarYear = model.CalendarYear;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Name = model.Name;
            Description = model.Description;
            Default = model.Default;

            if (model.ResultSet != null)
            {
                ResultSet = new ResultSetModel(model.ResultSet);
            }
        }
        
        public Guid ResultSetId { get; set; }
        
        public int CalendarYear { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public bool Default { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ExamSeasons.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}
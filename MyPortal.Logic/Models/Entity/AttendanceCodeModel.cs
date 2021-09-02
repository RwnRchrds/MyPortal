using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceCodeModel : BaseModel, ILoadable
    {
        public AttendanceCodeModel(AttendanceCode model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AttendanceCode model)
        {
            Code = model.Code;
            Description = model.Description;
            MeaningId = model.MeaningId;
            Active = model.Active;
            Restricted = model.Restricted;

            if (model.CodeMeaning != null)
            {
                CodeMeaning = new AttendanceCodeMeaningModel(model.CodeMeaning);
            }
        }
        
        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public Guid MeaningId { get; set; }

        public bool Active { get; set; }

        public bool Restricted { get; set; }

        public virtual AttendanceCodeMeaningModel CodeMeaning { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.AttendanceCodes.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

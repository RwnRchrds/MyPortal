using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceCodeModel : BaseModelWithLoad
    {
        public AttendanceCodeModel(AttendanceCode model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AttendanceCode model)
        {
            Code = model.Code;
            Description = model.Description;
            AttendanceCodeTypeId = model.AttendanceCodeTypeId;
            Active = model.Active;
            Restricted = model.Restricted;

            if (model.CodeType != null)
            {
                CodeType = new AttendanceCodeTypeModel(model.CodeType);
            }
        }
        
        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public Guid AttendanceCodeTypeId { get; set; }

        public bool Active { get; set; }

        public bool Restricted { get; set; }

        public virtual AttendanceCodeTypeModel CodeType { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.AttendanceCodes.GetById(Id.Value);

                if (model != null)
                {
                    LoadFromModel(model);
                }
            }
        }
    }
}

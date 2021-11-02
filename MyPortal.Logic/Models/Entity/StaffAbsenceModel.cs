using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class StaffAbsenceModel : BaseModel, ILoadable
    {
        public StaffAbsenceModel(StaffAbsence model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(StaffAbsence model)
        {
            StaffMemberId = model.StaffMemberId;
            AbsenceTypeId = model.AbsenceTypeId;
            IllnessTypeId = model.IllnessTypeId;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Confidential = model.Confidential;
            Notes = model.Notes;

            if (model.StaffMember != null)
            {
                StaffMember = new StaffMemberModel(model.StaffMember);
            }

            if (model.AbsenceType != null)
            {
                AbsenceType = new StaffAbsenceTypeModel(model.AbsenceType);
            }

            if (model.IllnessType != null)
            {
                IllnessType = new StaffIllnessTypeModel(model.IllnessType);
            }
        }
        
        public Guid StaffMemberId { get; set; }
        
        public Guid AbsenceTypeId { get; set; }
        
        public Guid? IllnessTypeId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public bool Confidential { get; set; }
        
        public string Notes { get; set; }

        public virtual StaffMemberModel StaffMember { get; set; }
        public virtual StaffAbsenceTypeModel AbsenceType { get; set; }
        public virtual StaffIllnessTypeModel IllnessType { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.StaffAbsences.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
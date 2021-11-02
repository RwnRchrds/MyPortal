using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class RegGroupModel : BaseModel, ILoadable
    {
        public RegGroupModel(RegGroup model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(RegGroup model)
        {
            StudentGroupId = model.StudentGroupId;
            YearGroupId = model.YearGroupId;

            if (model.StudentGroup != null)
            {
                StudentGroup = new StudentGroupModel(model.StudentGroup);
            }

            if (model.YearGroup != null)
            {
                YearGroup = new YearGroupModel(model.YearGroup);
            }
        }

        public Guid StudentGroupId { get; set; }
        
        public Guid YearGroupId { get; set; }

        public virtual StudentGroupModel StudentGroup { get; set; }

        public virtual YearGroupModel YearGroup { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.RegGroups.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class BulletinModel : BaseModel, ILoadable
    {
        public BulletinModel(Bulletin model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Bulletin model)
        {
            DirectoryId = model.DirectoryId;
            CreatedById = model.CreatedById;
            CreateDate = model.CreatedDate;
            ExpireDate = model.ExpireDate;
            Title = model.Title;
            Detail = model.Detail;
            StaffOnly = model.StaffOnly;
            Approved = model.Approved;

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }
        }
        
        public Guid DirectoryId { get; set; }
        
        public Guid CreatedById { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public DateTime? ExpireDate { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        
        [Required]
        public string Detail { get; set; }
        
        public bool StaffOnly { get; set; }
        
        public bool Approved { get; set; }

        public UserModel CreatedBy { get; set; }
        public DirectoryModel Directory { get; set; }

        public bool Expired => ExpireDate <= DateTime.Now;
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Bulletins.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

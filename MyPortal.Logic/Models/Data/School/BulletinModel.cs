using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Documents;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.School
{
    public class BulletinModel : BaseModelWithLoad
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
            Private = model.Private;
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

        [Required] [StringLength(128)] public string Title { get; set; }

        [Required] public string Detail { get; set; }

        public bool Private { get; set; }

        public bool Approved { get; set; }

        public UserModel CreatedBy { get; set; }
        public DirectoryModel Directory { get; set; }

        public bool Expired => ExpireDate <= DateTime.Now;

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var bulletin = await unitOfWork.Bulletins.GetById(Id.Value);

                if (bulletin != null)
                {
                    LoadFromModel(bulletin);
                }
            }
        }
    }
}
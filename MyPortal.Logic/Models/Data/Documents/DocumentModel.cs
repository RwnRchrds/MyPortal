using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Documents
{
    public class DocumentModel : BaseModelWithLoad
    {
        public DocumentModel(Document model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Document model)
        {
            TypeId = model.TypeId;
            DirectoryId = model.DirectoryId;
            Title = model.Title;
            Description = model.Description;
            CreatedById = model.CreatedById;
            CreatedDate = model.CreatedDate;
            Private = model.Private;
            Deleted = model.Deleted;
            FileId = model.FileId;

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }

            if (model.Type != null)
            {
                Type = new DocumentTypeModel(model.Type);
            }

            if (model.Attachment != null)
            {
                Attachment = new FileModel(model.Attachment);
            }
        }

        public Guid? FileId { get; set; }

        public Guid TypeId { get; set; }

        public Guid DirectoryId { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Private { get; set; }

        public bool Deleted { get; set; }

        public virtual UserModel CreatedBy { get; set; }
        public virtual DirectoryModel Directory { get; set; }
        public virtual DocumentTypeModel Type { get; set; }
        public virtual FileModel Attachment { get; set; }

        public DirectoryChildSummaryModel GetListModel()
        {
            return new DirectoryChildSummaryModel(this);
        }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Documents.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}

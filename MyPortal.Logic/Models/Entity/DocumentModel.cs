using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Collection;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class DocumentModel : BaseModel, ILoadable
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
            Restricted = model.Restricted;
            Confidential = model.Confidential;
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

        public bool Confidential { get; set; }

        public Guid TypeId { get; set; }

        public Guid DirectoryId { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Restricted { get; set; }

        public bool Deleted { get; set; }

        public virtual UserModel CreatedBy { get; set; }
        public virtual DirectoryModel Directory { get; set; }
        public virtual DocumentTypeModel Type { get; set; }
        public virtual FileModel Attachment { get; set; }

        public DirectoryChildListModel GetListModel()
        {
            return new DirectoryChildListModel(this);
        }

        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.Documents.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

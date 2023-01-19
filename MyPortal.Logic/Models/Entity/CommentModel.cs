using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class CommentModel : BaseModelWithLoad
    {
        public CommentModel(Comment model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Comment model)
        {
            CommentBankSectionId = model.CommentBankSectionId;
            Value = model.Value;

            if (model.Section != null)
            {
                Section = new CommentBankSectionModel(model.Section);
            }
        }
        
        public Guid CommentBankSectionId { get; set; }

        [Required]
        public string Value { get; set; }

        public CommentBankSectionModel Section { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var comment = await unitOfWork.Comments.GetById(Id.Value);

                if (comment != null)
                {
                    LoadFromModel(comment);
                }
            }
        }
    }
}

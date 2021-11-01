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
    public class CommentModel : BaseModel, ILoadable
    {
        public CommentModel(Comment model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Comment model)
        {
            CommentBankId = model.CommentBankId;
            Value = model.Value;

            if (model.CommentBank != null)
            {
                CommentBank = new CommentBankModel(model.CommentBank);
            }
        }
        
        public Guid CommentBankId { get; set; }

        [Required]
        public string Value { get; set; }

        public CommentBankModel CommentBank { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Comments.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

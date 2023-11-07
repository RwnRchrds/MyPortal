using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Profiles;

public class CommentBankAreaModel : BaseModel
{
    public CommentBankAreaModel(CommentBankArea model) : base(model)
    {
        LoadFromModel(model);
    }

    public void LoadFromModel(CommentBankArea model)
    {
        CommentBankId = model.CommentBankId;
        CourseId = model.CourseId;
        Name = model.Name;

        if (model.CommentBank != null)
        {
            CommentBank = new CommentBankModel(model.CommentBank);
        }

        if (model.Course != null)
        {
            Course = new CourseModel(model.Course);
        }
    }

    public async Task Load(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var commentBankArea = await unitOfWork.CommentBankAreas.GetById(Id.Value);

            LoadFromModel(commentBankArea);
        }
    }

    public Guid CommentBankId { get; set; }

    public Guid CourseId { get; set; }

    [Required] [StringLength(256)] public string Name { get; set; }

    public virtual CommentBankModel CommentBank { get; set; }
    public virtual CourseModel Course { get; set; }
}
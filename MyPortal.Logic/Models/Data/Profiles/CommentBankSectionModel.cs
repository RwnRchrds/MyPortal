using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Profiles;

public class CommentBankSectionModel : BaseModelWithLoad
{
    public CommentBankSectionModel(CommentBankSection model) : base(model)
    {
        LoadFromModel(model);
    }

    public void LoadFromModel(CommentBankSection model)
    {
        CommentBankAreaId = model.CommentBankAreaId;
        Name = model.Name;

        if (model.Area != null)
        {
            Area = new CommentBankAreaModel(model.Area);
        }
    }

    protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var section = await unitOfWork.CommentBankSections.GetById(Id.Value);

            if (section != null)
            {
                LoadFromModel(section);
            }
        }
    }

    public Guid CommentBankAreaId { get; set; }

    [Required] [StringLength(256)] public string Name { get; set; }

    public virtual CommentBankAreaModel Area { get; set; }
}
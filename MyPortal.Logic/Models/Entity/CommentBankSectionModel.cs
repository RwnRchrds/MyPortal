using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity;

public class CommentBankSectionModel : BaseModel
{
    public CommentBankSectionModel(CommentBankSection model)
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

    public async Task Load(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var section = await unitOfWork.CommentBankSections.GetById(Id.Value);
            
            LoadFromModel(section);
        }
    }
    
    public Guid CommentBankAreaId { get; set; }

    [Required]
    [StringLength(256)]
    public string Name { get; set; }
    
    public virtual CommentBankAreaModel Area { get; set; }
}
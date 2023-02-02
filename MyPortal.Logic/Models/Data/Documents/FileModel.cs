using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Documents
{
    public class FileModel : BaseModel
    {
        public FileModel(File model) : base(model)
        {
            FileId = model.FileId;
            FileName = model.FileName;
            ContentType = model.ContentType;
        }

        [Required]
        public string FileId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }
    }
}

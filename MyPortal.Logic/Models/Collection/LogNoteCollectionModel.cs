using System;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Collection
{
    public class LogNoteCollectionModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
        public string LogTypeName { get; set; }
        public string LogTypeIcon { get; set; }
        public string LogTypeColourCode { get; set; }
        public string Message { get; set; }

        public LogNoteCollectionModel(LogNoteModel model)
        {
            if (model.Id.HasValue)
            {
                Id = model.Id.Value;   
            }
            CreatedDate = model.CreatedDate;
            AuthorName = model.CreatedBy.GetDisplayName(NameFormat.FullNameAbbreviated);
            LogTypeName = model.LogNoteType.Description;
            LogTypeIcon = model.LogNoteType.IconClass;
            LogTypeColourCode = model.LogNoteType.ColourCode;
            Message = model.Message;
        }
    }
}
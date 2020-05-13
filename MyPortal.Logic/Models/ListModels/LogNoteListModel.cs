using System;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Models.Summary
{
    public class LogNoteListModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string LogTypeName { get; set; }
        public string LogTypeIcon { get; set; }
        public string LogTypeColourCode { get; set; }
        public string Message { get; set; }

        public LogNoteListModel(LogNoteModel model)
        {
            Id = model.Id;
            Date = model.Date;
            AuthorName = model.Author.Person.GetDisplayName(true);
            LogTypeName = model.LogNoteType.Name;
            LogTypeIcon = model.LogNoteType.GetIcon();
            LogTypeColourCode = model.LogNoteType.ColourCode;
            Message = model.Message;
        }
    }
}
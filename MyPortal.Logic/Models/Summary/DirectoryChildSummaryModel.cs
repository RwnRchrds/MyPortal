using System;
using MyPortal.Logic.Models.Data.Documents;

namespace MyPortal.Logic.Models.Summary
{
    public class DirectoryChildSummaryModel
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsDirectory { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DirectoryChildSummaryModel(DirectoryModel directory)
        {
            if (directory.Id.HasValue)
            {
                Id = directory.Id.Value;
            }

            ParentId = directory.ParentId;
            IsDirectory = true;
            Name = directory.Name;
            Type = "Directory";
        }

        public DirectoryChildSummaryModel(DocumentModel document)
        {
            if (document.Id.HasValue)
            {
                Id = document.Id.Value;
            }

            ParentId = document.DirectoryId;
            IsDirectory = false;
            Name = document.Title;
            CreatedDate = document.CreatedDate;
            Type = document.Type.Description;
        }
    }
}
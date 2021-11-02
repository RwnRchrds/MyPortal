using System;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Collection
{
    public class DirectoryChildCollectionModel
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsDirectory { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DirectoryChildCollectionModel(DirectoryModel directory)
        {
            if (directory.Id.HasValue)
            {
                Id = directory.Id.Value;   
            }
            ParentId = directory.ParentId;
            IsDirectory = true;
            Name = directory.Name;
            Icon = Icons.Files.Directory;
            Type = "Directory";
        }

        public DirectoryChildCollectionModel(DocumentModel document)
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
            Icon = Icons.Files.File;
        }
    }
}

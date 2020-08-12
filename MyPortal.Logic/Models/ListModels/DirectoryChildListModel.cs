using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.ListModels
{
    public class DirectoryChildListModel
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsDirectory { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DirectoryChildListModel(DirectoryModel directory)
        {
            Id = directory.Id;
            ParentId = directory.ParentId;
            IsDirectory = true;
            Name = directory.Name;
            Icon = Icons.Files.Directory;
            Type = "Directory";
        }

        public DirectoryChildListModel(DocumentModel document)
        {
            Id = document.Id;
            ParentId = document.DirectoryId;
            IsDirectory = false;
            Name = document.Title;
            CreatedDate = document.CreatedDate;
            Type = document.Type.Description;
            Icon = Icons.Files.File;
        }
    }
}

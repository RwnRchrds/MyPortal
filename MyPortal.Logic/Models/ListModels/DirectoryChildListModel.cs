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

            if (document.ContentType == GoogleMimeTypes.GoogleDocs || MimeTypeHelper.IsWord(document.ContentType))
            {
                Icon = Icons.Files.Doc;
            }
            else if (document.ContentType == GoogleMimeTypes.GoogleSlides || MimeTypeHelper.GetExtension(document.ContentType, false).StartsWith(".ppt"))
            {
                Icon = Icons.Files.Ppt;
            }
            else if (document.ContentType == GoogleMimeTypes.GoogleSheets || MimeTypeHelper.IsExcel(document.ContentType))
            {
                Icon = Icons.Files.Xls;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType, false) == ".jpg")
            {
                Icon = Icons.Files.Jpg;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType, false).StartsWith(".mp3"))
            {
                Icon = Icons.Files.Mp3;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType, false).StartsWith(".pdf"))
            {
                Icon = Icons.Files.Pdf;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType, false).StartsWith(".png"))
            {
                Icon = Icons.Files.Png;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType, false).StartsWith(".zip"))
            {
                Icon = Icons.Files.Zip;
            }
            else
            {
                Icon = Icons.Files.File;
            }
        }
    }
}

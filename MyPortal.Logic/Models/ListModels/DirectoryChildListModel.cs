using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Business;

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
            CreatedDate = document.UploadedDate;
            Type = document.Type.Description;

            if (document.ContentType == GoogleMimeTypes.GoogleDocs || MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".doc"))
            {
                Icon = Icons.Files.Doc;
            }
            else if (document.ContentType == GoogleMimeTypes.GoogleSlides || MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".ppt"))
            {
                Icon = Icons.Files.Ppt;
            }
            else if (document.ContentType == GoogleMimeTypes.GoogleSheets || MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".xls"))
            {
                Icon = Icons.Files.Xls;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".jpg"))
            {
                Icon = Icons.Files.Jpg;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".mp3"))
            {
                Icon = Icons.Files.Mp3;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".pdf"))
            {
                Icon = Icons.Files.Pdf;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".png"))
            {
                Icon = Icons.Files.Png;
            }
            else if (MimeTypeHelper.GetExtension(document.ContentType).StartsWith(".zip"))
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

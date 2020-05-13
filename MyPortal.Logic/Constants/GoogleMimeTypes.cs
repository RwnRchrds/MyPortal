using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Constants
{
    public class GoogleMimeTypes
    {
        public const string GoogleDocs = "application/vnd.google-apps.document";
        public const string GoogleDrawing = "application/vnd.google-apps.drawing";
        public const string GoogleDriveFile = "application/vnd.google-apps.file";
        public const string GoogleDriveFolder = "application/vnd.google-apps.folder";
        public const string GoogleForms = "application/vnd.google-apps.form";
        public const string GoogleFusionTables = "application/vnd.google-apps.fusiontable";
        public const string GoogleMyMaps = "application/vnd.google-apps.map";
        public const string GoogleSlides = "application/vnd.google-apps.presentation";
        public const string GoogleAppsScripts = "application/vnd.google-apps.script";
        public const string GoogleSites = "application/vnd.google-apps.site";
        public const string GoogleSheets = "application/vnd.google-apps.spreadsheet";

        public static List<string> GetAll()
        {
            return new List<string>
            {
                GoogleDocs,
                GoogleDrawing,
                GoogleDriveFile,
                GoogleDriveFolder,
                GoogleForms,
                GoogleFusionTables,
                GoogleMyMaps,
                GoogleSlides,
                GoogleAppsScripts,
                GoogleSites,
                GoogleSheets
            };
        }

        public static string GetExportMimeType(string mimeType)
        {
            switch (mimeType)
            {
                case GoogleDocs:
                    return MimeTypeHelper.GetMimeType(".docx");
                case GoogleSheets:
                    return MimeTypeHelper.GetMimeType(".xlsx");
                case GoogleSlides:
                    return MimeTypeHelper.GetMimeType(".pptx");
                default:
                    return "application/octet-stream";
            }
        }

        public static string GetExtension(string mimeType, bool alternative)
        {
            if (alternative)
            {
                switch (mimeType)
                {
                    case GoogleDocs:
                        return ".docx";
                    case GoogleSheets:
                        return ".xlsx";
                    case GoogleSlides:
                        return ".pptx";
                    default:
                        throw new ArgumentException($"Mimetype is not registered: {mimeType}");
                }
            }

            switch (mimeType)
            {
                case GoogleDocs:
                    return ".gdoc";
                case GoogleSheets:
                    return ".gsheet";
                case GoogleSlides:
                    return ".gslides";
                default:
                    throw new ArgumentException($"Mimetype is not registered: {mimeType}");
            }
        }
    }
}

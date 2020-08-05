using System.IO;

namespace MyPortal.Logic.Models.DocumentProvision
{
    public class FileDownload
    {
        public FileDownload(Stream fileStream, string contentType, string fileName)
        {
            FileStream = fileStream;
            ContentType = contentType;
            FileName = fileName;
        }

        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}

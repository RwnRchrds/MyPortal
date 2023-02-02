using System;
using System.IO;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Documents
{
    public class PhotoModel : BaseModel
    {
        public PhotoModel(Photo model) : base(model)
        {
            Data = model.Data;
            PhotoDate = model.PhotoDate;
            MimeType = model.MimeType;
        }

        public Stream GetStream()
        {
            var stream = new MemoryStream(Data);

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
        
        public byte[] Data { get; set; }

        public DateTime PhotoDate { get; set; }

        public string MimeType { get; set; }
    }
}

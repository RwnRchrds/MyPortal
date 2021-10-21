using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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

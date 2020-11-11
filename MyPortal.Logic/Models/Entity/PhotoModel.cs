using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class PhotoModel : BaseModel
    {
        public byte[] Data { get; set; }

        public DateTime PhotoDate { get; set; }

        public string MimeType { get; set; }
    }
}

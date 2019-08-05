using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.GridDtos
{
    public class GridDocumentDto
    {
        public int DocumentId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool Approved { get; set; }
    }
}
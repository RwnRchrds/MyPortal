using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.GridDtos
{
    public class GridLogDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string LogTypeName { get; set; }
        public string Message { get; set; }
    }
}
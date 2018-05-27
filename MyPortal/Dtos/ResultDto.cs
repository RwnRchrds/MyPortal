using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class ResultDto
    {
        public int ResultSet { get; set; }
        public int Subject { get; set; }
        public string Value { get; set; }
        public int Student { get; set; }
    }
}
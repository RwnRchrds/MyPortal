using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }
        public int QsiKs3 { get; set; }
        public int QsiKs4 { get; set; }
        public int FourMIdKs3 { get; set; }
        public int FourMIdKs4 { get; set; }
    }
}
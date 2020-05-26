using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Models.Filters
{
    public class DocumentTypeFilter
    {
        public bool Student { get; set; }
        public bool Staff { get; set; }
        public bool Contact { get; set; }
        public bool General { get; set; }
        public bool Sen { get; set; }
        public bool Active { get; set; }
    }
}

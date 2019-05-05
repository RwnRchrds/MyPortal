using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.Models.Misc
{
    public class ListContainer<T>
    {
        public IEnumerable<T> Objects { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.ViewModels
{
    public class SalesViewModel
    {
        public SalesViewModel()
        {
            Status = new List<string>() {"All","Pending Only"};    
        }

        public IEnumerable<string> Status { get; set; }
    }
}
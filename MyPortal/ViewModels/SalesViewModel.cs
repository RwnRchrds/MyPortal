using System.Collections.Generic;

namespace MyPortal.ViewModels
{
    public class SalesViewModel
    {
        public SalesViewModel()
        {
            Status = new List<string> {"All", "Pending Only"};
        }

        public IEnumerable<string> Status { get; set; }
    }
}
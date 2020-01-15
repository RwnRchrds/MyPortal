using System.Collections.Generic;

namespace MyPortal.Areas.Staff.ViewModels.Finance
{
    public class SalesViewModel
    {
        public SalesViewModel()
        {
            Status = new List<string> {"All", "Pending", "Processed"};
        }

        public IEnumerable<string> Status { get; set; }
    }
}
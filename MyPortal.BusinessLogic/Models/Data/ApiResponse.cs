using System.Collections.Generic;

namespace MyPortal.BusinessLogic.Models.Data
{
    public class ApiResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
    }
}
using System.Collections.Generic;

namespace MyPortal.Logic.Models.Business
{
    public class ApiResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
    }
}
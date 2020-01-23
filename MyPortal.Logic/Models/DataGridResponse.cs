using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models
{
    public class DataGridResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
    }
}

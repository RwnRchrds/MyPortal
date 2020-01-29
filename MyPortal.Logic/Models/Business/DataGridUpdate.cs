using System.Collections.Generic;

namespace MyPortal.Logic.Models.Business
{
    public class DataGridUpdate<T>
    {
        public List<T> Added { get; set; }
        public List<T> Changed { get; set; }

        public List<T> Deleted { get; set; }

        public T Value { get; set; }

        public int? Key { get; set; }

        public string Action { get; set; }
    }
}
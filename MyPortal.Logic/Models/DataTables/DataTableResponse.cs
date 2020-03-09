using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.DataTables
{
    public class DataTableResponse
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable Data { get; set; }
        public string Error { get; set; }
    }
}

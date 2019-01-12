using System.Collections;
using System.Collections.Generic;
using System.Web;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class ImportResultsViewModel
    {
        public IEnumerable<ResultSet> ResultSets { get; set; }
        public ResultSet ResultSet { get; set; }        
        public bool FileExists { get; set; }
        public int ResultsToImport { get; set; }
    }
}
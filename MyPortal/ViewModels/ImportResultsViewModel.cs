using System.Collections;
using System.Collections.Generic;
using System.Web;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class ImportResultsViewModel
    {
        public IEnumerable<AssessmentResultSet> ResultSets { get; set; }
        public AssessmentResultSet ResultSet { get; set; }        
        public bool FileExists { get; set; }
        public int ResultsToImport { get; set; }
    }
}
using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class ImportResultsViewModel
    {
        public IEnumerable<AssessmentResultSet> ResultSets { get; set; }
        public AssessmentResultSet ResultSet { get; set; }        
        public bool FileExists { get; set; }
        public int ResultsToImport { get; set; }
    }
}
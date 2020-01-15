using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels.Assessment
{
    public class ImportResultsViewModel
    {
        public IEnumerable<ResultSetDto> ResultSets { get; set; }
        public ResultSetDto ResultSet { get; set; }
        public bool FileExists { get; set; }
        public int ResultsToImport { get; set; }
    }
}
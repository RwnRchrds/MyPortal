using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Students.ViewModels
{
    public class StudentResultsViewModel
    {
        public StudentDto Student { get; set; }
        public IEnumerable<ResultSetDto> ResultSets { get; set; }
        public ResultDto Result { get; set; }
        public IEnumerable<SubjectDto> Subjects { get; set; }
    }
}
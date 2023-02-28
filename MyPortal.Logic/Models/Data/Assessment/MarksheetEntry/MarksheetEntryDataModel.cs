using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.QueryResults.Assessment;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Data.Assessment.MarksheetEntry;

public class MarksheetEntryDataModel
{
    public MarksheetEntryDataModel()
    {
        Columns = new List<MarksheetColumnDataModel>();
        Students = new List<MarksheetStudentDataModel>();
    }
    
    public string Title { get; set; }
    public bool Completed { get; set; }

    public ICollection<MarksheetColumnDataModel> Columns { get; set; }
    public ICollection<MarksheetStudentDataModel> Students { get; set; }

    public void PopulateResults(IEnumerable<ResultDetailModel> results)
    {
        var data = new List<MarksheetStudentDataModel>();

        var students = results.GroupBy(r => r.StudentId).ToArray();

        foreach (var student in students)
        {
            var studentData = student.ToArray();

            var dataRow = new MarksheetStudentDataModel();

            dataRow.StudentId = student.Key;

            for (int i = 0; i < studentData.Length; i++)
            {
                var result = studentData[i];

                if (i == 0)
                {
                    dataRow.StudentName = result.StudentName;
                }
                
                dataRow.Results.Add(new ResultSummaryModel
                {
                    StudentId = result.StudentId,
                    ResultSetId = result.ResultSetId,
                    AspectId = result.AspectId,
                    GradeId = result.GradeId,
                    Mark = result.Mark,
                    Comment = result.Comment,
                    Note = result.Note,
                    ColourCode = result.ColourCode
                });
            }
            
            data.Add(dataRow);
        }

        Students = data.OrderBy(d => d.StudentName).ToArray();
    }
    
}
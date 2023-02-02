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

    public async Task PopulateColumns(IUnitOfWork unitOfWork, IEnumerable<MarksheetColumnModel> columnCollection)
    {
        Dictionary<Guid, GradeModel[]> gradeSets = new Dictionary<Guid, GradeModel[]>();

        foreach (var columnModel in columnCollection)
        {
            if (columnModel.Aspect.GradeSetId.HasValue && !gradeSets.ContainsKey(columnModel.Aspect.GradeSetId.Value))
            {
                var grades = (await unitOfWork.Grades.GetByGradeSet(columnModel.Aspect.GradeSetId.Value))
                    .Select(g => new GradeModel(g)).ToArray();
                
                if (grades.Any())
                {
                    gradeSets.Add(columnModel.Aspect.GradeSetId.Value, grades);
                }
            }

            var column = new MarksheetColumnDataModel
            {
                Header = columnModel.Aspect.ColumnHeading,
                ResultSetId = columnModel.ResultSetId,
                ResultSetName = columnModel.ResultSet.Name,
                AspectTypeId = columnModel.Aspect.TypeId,
                Order = columnModel.DisplayOrder,
                AspectId = columnModel.AspectId,
                IsReadOnly = columnModel.ResultSet.Locked || columnModel.ReadOnly || Completed
            };

            var aspectType = columnModel.Aspect.TypeId;

            if (aspectType == AspectTypes.Grade)
            {
                if (!columnModel.Aspect.GradeSetId.HasValue ||
                    !gradeSets.TryGetValue(columnModel.Aspect.GradeSetId.Value, out var columnGrades))
                {
                    throw new NotFoundException("Grade set not found.");
                }

                column.Grades = columnGrades;
            }
            else if (aspectType == AspectTypes.MarkDecimal || aspectType == AspectTypes.MarkInteger)
            {
                column.MinMark = columnModel.Aspect.MinMark;
                column.MaxMark = columnModel.Aspect.MaxMark;
            }
            
            Columns.Add(column);
        }
    }

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
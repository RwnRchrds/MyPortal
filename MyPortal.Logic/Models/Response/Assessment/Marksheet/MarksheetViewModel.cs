using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Response.Assessment.Marksheet;

public class MarksheetViewModel
{
    public MarksheetViewModel()
    {
        Columns = new List<MarksheetColumnViewModel>();
        Students = new List<MarksheetStudentViewModel>();
    }
    
    public string Title { get; set; }

    public ICollection<MarksheetColumnViewModel> Columns { get; set; }
    public ICollection<MarksheetStudentViewModel> Students { get; set; }

    public async Task PopulateColumns(IUnitOfWork unitOfWork, IEnumerable<MarksheetColumnModel> columnCollection)
    {
        // TODO: Continue work on this
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

            var column = new MarksheetColumnViewModel
            {
                Header = columnModel.Aspect.ColumnHeading,
                ResultSetId = columnModel.ResultSetId,
                ResultSetName = columnModel.ResultSet.Name,
                AspectTypeId = columnModel.Aspect.TypeId,
                Order = columnModel.DisplayOrder,
                AspectId = columnModel.AspectId
            };

            var aspectType = columnModel.Aspect.TypeId;

            if (aspectType == AspectTypes.Grade)
            {
                if (!gradeSets.TryGetValue(columnModel.Aspect.GradeSetId.Value, out var columnGrades))
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
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static partial class AssessmentProcesses
    {
        public static ProcessResponse<object> CreateResultSet(AssessmentResultSet resultSet, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(resultSet))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var currentRsExists = context.AssessmentResultSets.Any(x => x.IsCurrent);

            if (!currentRsExists)
            {
                resultSet.IsCurrent = true;
            }

            context.AssessmentResultSets.Add(resultSet);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Result set created", null);
        }
        
        public static ProcessResponse<object> UpdateResultSet(AssessmentResultSet resultSet, MyPortalDbContext context)
        {
            var resultSetInDb = context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSet.Id);

            if (resultSetInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Result set not found", null);
            }

            resultSetInDb.Name = resultSet.Name;

            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Result set updated", null);
        }

        public static ProcessResponse<object> DeleteResultSet(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Result set not found", null);
            }

            if (resultSet.IsCurrent)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Result set is already marked as current", null);
            }

            context.AssessmentResultSets.Remove(resultSet);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Result set deleted", null);
        }

        public static ProcessResponse<AssessmentResultSetDto> GetResultSetById(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = GetResultSetById_Model(resultSetId, context);

            if (resultSet.ResponseType == ResponseType.NotFound)
            {
                return new ProcessResponse<AssessmentResultSetDto>(ResponseType.NotFound, resultSet.ResponseMessage, null);
            }

            if (resultSet.ResponseObject == null)
            {
                return new ProcessResponse<AssessmentResultSetDto>(ResponseType.NotFound, "Result set not found", null);
            }

            return new ProcessResponse<AssessmentResultSetDto>(ResponseType.Ok, null,
                Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>(resultSet.ResponseObject));

        }

        public static ProcessResponse<AssessmentResultSet> GetResultSetById_Model(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return new ProcessResponse<AssessmentResultSet>(ResponseType.NotFound, "Result set not found", null);
            }

            return new ProcessResponse<AssessmentResultSet>(ResponseType.Ok, null,
                resultSet);

        }

        public static ProcessResponse<IEnumerable<GridAssessmentResultSetDto>> GetAllResultSets_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridAssessmentResultSetDto>>(ResponseType.Ok, null,
                GetAllResultSets_Model(context).ResponseObject
                    .Select(Mapper.Map<AssessmentResultSet, GridAssessmentResultSetDto>));
        }

        public static ProcessResponse<IEnumerable<AssessmentResultSetDto>> GetAllResultSets(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<AssessmentResultSetDto>>(ResponseType.Ok, null,
                GetAllResultSets_Model(context).ResponseObject
                    .Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>));
        }

        public static ProcessResponse<IEnumerable<AssessmentResultSet>> GetAllResultSets_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<AssessmentResultSet>>(ResponseType.Ok, null,
                context.AssessmentResultSets.OrderBy(x => x.Name).ToList());
        }

        public static ProcessResponse<bool> ResultSetContainsResults(int id, MyPortalDbContext context)
        {
            var resultSet = context.AssessmentResultSets.SingleOrDefault(x => x.Id == id);

            if (resultSet == null)
            {
                throw new ProcessException("Result set not found", ExceptionType.NotFound);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, resultSet.AssessmentResults.Any());
        }

        public static ProcessResponse<object> SetResultSetAsCurrent(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Result set not found", null);
            }

            if (resultSet.IsCurrent)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Result set already set as current", null);
            }

            var currentResultSet = context.AssessmentResultSets.SingleOrDefault(x => x.IsCurrent);

            if (currentResultSet == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Result set not found", null);
            }

            currentResultSet.IsCurrent = false;
            resultSet.IsCurrent = true;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Result set set as current", null);
        }

        public static ProcessResponse<object> ImportResultsToResultSet(int resultSetId, MyPortalDbContext context)
        {
            if (!File.Exists(@"C:\MyPortal\Files\Results\import.csv"))
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "File not found", null);
            }

            var stream = new FileStream(@"C:\MyPortal\Files\Results\import.csv", FileMode.Open);
            var subjects = context.CurriculumSubjects.OrderBy(x => x.Name).ToList();
            var numResults = 0;
            var resultSet = context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Result set not found", null);
            }

            using (var reader = new StreamReader(stream))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        continue;
                    }

                    var values = line.Split(',');
                    for (var i = 0; i < subjects.Count; i++)
                    {
                        var studentMisId = values[4];
                        var student = context.Students.SingleOrDefault(x => x.MisId == studentMisId);
                        if (student == null)
                        {
                            continue;
                        }

                        var result = new AssessmentResult
                        {
                            StudentId = student.Id,
                            ResultSetId = resultSet.Id,
                            SubjectId = subjects[i].Id,
                            Value = values[5 + i]
                        };

                        if (result.Value.Equals(""))
                        {
                            continue;
                        }

                        CreateResult(result, context, false);
                        numResults++;
                    }
                }
            }
            
            context.SaveChanges();

            stream.Dispose();

            var guid = Guid.NewGuid();
            File.Move(@"C:/MyPortal/Files/Results/import.csv", @"C:/MyPortal/Files/Results/" + guid + "_IMPORTED.csv");
            
            return new ProcessResponse<object>(ResponseType.Ok, numResults + " results found and imported", null);
        }

        public static ProcessResponse<object> CreateResult(AssessmentResult result, MyPortalDbContext context, bool commitImmediately = true)
        {
            if (!ValidationProcesses.ModelIsValid(result))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            if (!context.AssessmentGrades.Any(x => x.GradeValue == result.Value))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Grade does not exist", null);
            }
            
            var resultInDb = context.AssessmentResults.SingleOrDefault(x =>
                x.StudentId == result.StudentId && x.SubjectId == result.SubjectId && x.ResultSetId == result.ResultSetId);

            if (resultInDb != null)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Result already exists", null);
            }

            context.AssessmentResults.Add(result);
            
            if (commitImmediately)
            {
                context.SaveChanges();   
            }

            return new ProcessResponse<object>(ResponseType.Ok, "Result added", null);
        }

        public static ProcessResponse<IEnumerable<AssessmentResultDto>> GetResultsByStudent(int studentId, int resultSetId,
            MyPortalDbContext context)
        {
            var results = context.AssessmentResults
                .Where(r => r.StudentId == studentId && r.ResultSetId == resultSetId)
                .ToList()
                .Select(Mapper.Map<AssessmentResult, AssessmentResultDto>);

            return new ProcessResponse<IEnumerable<AssessmentResultDto>>(ResponseType.Ok, null, results);
        }

        public static ProcessResponse<IEnumerable<AssessmentResult>> GetResultsForStudent_Model(int studentId, int resultSetId,
            MyPortalDbContext context)
        {
            var results = context.AssessmentResults
                .Where(r => r.StudentId == studentId && r.ResultSetId == resultSetId)
                .ToList();

            return new ProcessResponse<IEnumerable<AssessmentResult>>(ResponseType.Ok, null, results);
        }
    }
}
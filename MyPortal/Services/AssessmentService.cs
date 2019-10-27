using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Services
{
    public class AssessmentService : MyPortalService
    {
        public AssessmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task CreateResult(AssessmentResult result)
        {
            if (!ValidationService.ModelIsValid(result))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            _unitOfWork.AssessmentResults.Add(result);
            await _unitOfWork.Complete();
        }

        public async Task CreateResultSet(AssessmentResultSet resultSet)
        {
            if (!ValidationService.ModelIsValid(resultSet))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var currentRsExists = await _unitOfWork.AssessmentResultSets.AnyAsync(x => x.IsCurrent);

            if (!currentRsExists)
            {
                resultSet.IsCurrent = true;
            }

            _unitOfWork.AssessmentResultSets.Add(resultSet);
            await _unitOfWork.Complete();
        }

        public async Task DeleteResultSet(int resultSetId)
        {
            var resultSet = await _unitOfWork.AssessmentResultSets.GetByIdAsync(resultSetId);

            if (resultSet == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Result set is marked as current");
            }

            _unitOfWork.AssessmentResultSets.Remove(resultSet);
            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<AssessmentResultSetDto>> GetAllResultSetsDto()
        {
            var resultSets = await _unitOfWork.AssessmentResultSets.GetAllAsync();

            return resultSets.Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>);
        }

        public async Task<IEnumerable<GridAssessmentResultSetDto>> GetAllResultSetsDataGrid()
        {
            var resultSets = await _unitOfWork.AssessmentResultSets.GetAllAsync();

            return resultSets.Select(Mapper.Map<AssessmentResultSet, GridAssessmentResultSetDto>);
        }

        public async Task<IEnumerable<AssessmentResultSet>> GetAllResultSets()
        {
            return await _unitOfWork.AssessmentResultSets.GetAllAsync();
        }

        public async Task<AssessmentResultDto> GetResultById(int resultId)
        {
            var result = await _unitOfWork.AssessmentResults.GetByIdAsync(resultId);

            return Mapper.Map<AssessmentResult, AssessmentResultDto>(result);
        }

        public async Task<IEnumerable<AssessmentResult>> GetResultsByStudent(int studentId, int resultSetId)
        {
            var results = await _unitOfWork.AssessmentResults.GetResultsByStudent(studentId, resultSetId);

            return results;
        }

        public async Task<IEnumerable<AssessmentResultDto>> GetResultsByStudentDto(int studentId, int resultSetId)
        {
            var results = await GetResultsByStudent(studentId, resultSetId);

            return results.Select(Mapper.Map<AssessmentResult, AssessmentResultDto>);
        }

        public async Task<IEnumerable<GridAssessmentResultDto>> GetResultsByStudentDataGrid(int studentId, int resultSetId)
        {
            var results = await GetResultsByStudent(studentId, resultSetId);

            return results.Select(Mapper.Map<AssessmentResult, GridAssessmentResultDto>);
        }

        public async Task<AssessmentResultSetDto> GetResultSetByIdDto(int resultSetId)
        {
            var resultSet = await GetResultSetById(resultSetId);

            return Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>(resultSet);
        }

        public async Task<AssessmentResultSet> GetResultSetById(int resultSetId)
        {
            var resultSet = await _unitOfWork.AssessmentResultSets.GetByIdAsync(resultSetId);

            if (resultSet == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            return resultSet;
        }

        public async Task<IEnumerable<AssessmentResultSetDto>> GetResultSetsByStudentDto(int studentId)
        {
            var resultSets = await GetResultSetsByStudent(studentId);

            return resultSets.Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>);
        }

        public async Task<IEnumerable<AssessmentResultSet>> GetResultSetsByStudent(int studentId)
        {
            if (!await _unitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var resultSets = await _unitOfWork.AssessmentResultSets.GetResultSetsByStudent(studentId);

            return resultSets;
        }

        public async Task<bool> ResultSetContainsResults(int resultSetId)
        {
            if (!await _unitOfWork.AssessmentResultSets.AnyAsync(x => x.Id == resultSetId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            return await _unitOfWork.AssessmentResults.AnyAsync(x => x.ResultSetId == resultSetId);
        }

        public async Task SetResultSetAsCurrent(int resultSetId)
        {
            var resultSet = await _unitOfWork.AssessmentResultSets.GetByIdAsync(resultSetId);

            if (resultSet == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Result set is already marked as current");
            }

            var currentResultSet = await _unitOfWork.AssessmentResultSets.GetCurrent();

            if (currentResultSet != null)
            {
                currentResultSet.IsCurrent = false;
            }

            resultSet.IsCurrent = true;

            await _unitOfWork.Complete();
        }

        public async Task UpdateResultSet(AssessmentResultSet resultSet)
        {
            var resultSetInDb = await _unitOfWork.AssessmentResultSets.GetByIdAsync(resultSet.Id);

            if (resultSetInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            resultSetInDb.Name = resultSet.Name;

            await _unitOfWork.Complete();
        }

        public async Task<IDictionary<int, string>> GetAllResultSetsLookup(MyPortalDbContext context)
        {
            var resultSets = await GetAllResultSets();

            return resultSets.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
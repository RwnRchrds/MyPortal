using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public AssessmentService() : base()
        {

        }

        public async Task CreateResult(AssessmentResult result)
        {
            if (!ValidationService.ModelIsValid(result))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.AssessmentResults.Add(result);
            await UnitOfWork.Complete();
        }

        public async Task CreateResultSet(AssessmentResultSet resultSet)
        {
            if (!ValidationService.ModelIsValid(resultSet))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            UnitOfWork.AssessmentResultSets.Add(resultSet);
            await UnitOfWork.Complete();
        }

        public async Task DeleteResultSet(int resultSetId)
        {
            var resultSet = await GetResultSetById(resultSetId);

            UnitOfWork.AssessmentResultSets.Remove(resultSet);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<AssessmentResultSet>> GetAllResultSets()
        {
            return await UnitOfWork.AssessmentResultSets.GetAll();
        }

        public async Task<AssessmentResult> GetResultById(int resultId)
        {
            var result = await UnitOfWork.AssessmentResults.GetById(resultId);

            return result;
        }

        public async Task<IEnumerable<AssessmentResult>> GetResultsByStudent(int studentId, int resultSetId)
        {
            var results = await UnitOfWork.AssessmentResults.GetResultsByStudent(studentId, resultSetId);

            return results;
        }

        public async Task<AssessmentResultSet> GetResultSetById(int resultSetId)
        {
            var resultSet = await UnitOfWork.AssessmentResultSets.GetById(resultSetId);

            if (resultSet == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Result set not found");
            }

            return resultSet;
        }

        public async Task<IEnumerable<AssessmentResultSet>> GetResultSetsByStudent(int studentId)
        {
            var resultSets = await UnitOfWork.AssessmentResultSets.GetResultSetsByStudent(studentId);

            return resultSets;
        }

        public async Task UpdateResultSet(AssessmentResultSet resultSet)
        {
            var resultSetInDb = await GetResultSetById(resultSet.Id);

            resultSetInDb.Name = resultSet.Name;

            await UnitOfWork.Complete();
        }

        public async Task<IDictionary<int, string>> GetAllResultSetsLookup(MyPortalDbContext context)
        {
            var resultSets = await GetAllResultSets();

            return resultSets.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
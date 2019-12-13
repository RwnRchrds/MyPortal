using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class AssessmentService : MyPortalService
    {
        public AssessmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AssessmentService() : base()
        {

        }

        public async Task CreateResult(Result result)
        {
            ValidationService.ValidateModel(result);

            UnitOfWork.Results.Add(result);
            await UnitOfWork.Complete();
        }

        public async Task CreateResultSet(ResultSet resultSet)
        {
            ValidationService.ValidateModel(resultSet);

            UnitOfWork.ResultSets.Add(resultSet);
            await UnitOfWork.Complete();
        }

        public async Task DeleteResultSet(int resultSetId)
        {
            var resultSet = await GetResultSetById(resultSetId);

            UnitOfWork.ResultSets.Remove(resultSet);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<ResultSet>> GetAllResultSets()
        {
            return await UnitOfWork.ResultSets.GetAll(x => x.Name);
        }

        public async Task<Result> GetResultById(int resultId)
        {
            var result = await UnitOfWork.Results.GetById(resultId);

            return result;
        }

        public async Task<IEnumerable<Result>> GetResultsByStudent(int studentId, int resultSetId)
        {
            var results = await UnitOfWork.Results.GetResultsByStudent(studentId, resultSetId);

            return results;
        }

        public async Task<ResultSet> GetResultSetById(int resultSetId)
        {
            var resultSet = await UnitOfWork.ResultSets.GetById(resultSetId);

            if (resultSet == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Result set not found");
            }

            return resultSet;
        }

        public async Task<IEnumerable<ResultSet>> GetResultSetsByStudent(int studentId)
        {
            var resultSets = await UnitOfWork.ResultSets.GetResultSetsByStudent(studentId);

            return resultSets;
        }

        public async Task UpdateResultSet(ResultSet resultSet)
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
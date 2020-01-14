﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
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

        public async Task CreateResult(ResultDto result)
        {
            ValidationService.ValidateModel(result);

            UnitOfWork.Results.Add(Mapper.Map<Result>(result));
            await UnitOfWork.Complete();
        }

        public async Task CreateResultSet(ResultSetDto resultSet)
        {
            ValidationService.ValidateModel(resultSet);

            UnitOfWork.ResultSets.Add(Mapper.Map<ResultSet>(resultSet));
            await UnitOfWork.Complete();
        }

        public async Task DeleteResultSet(int resultSetId)
        {
            var resultSet = await UnitOfWork.ResultSets.GetById(resultSetId);

            if (resultSet == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Result set not found.");
            }

            UnitOfWork.ResultSets.Remove(resultSet);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<ResultSetDto>> GetAllResultSets()
        {
            return (await UnitOfWork.ResultSets.GetAll(x => x.Name)).Select(Mapper.Map<ResultSetDto>).ToList();
        }

        public async Task<ResultDto> GetResultById(int resultId)
        {
            return Mapper.Map<ResultDto>(await UnitOfWork.Results.GetById(resultId));
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByStudent(int studentId, int resultSetId)
        {
            return (await UnitOfWork.Results.GetByStudent(studentId, resultSetId)).Select(Mapper.Map<ResultDto>).ToList();
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByAspect(int aspectId)
        {
            return (await UnitOfWork.Results.GetByAspect(aspectId)).Select(Mapper.Map<ResultDto>).ToList();
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByResultSet(int resultSetId)
        {
            return (await UnitOfWork.Results.GetByResultSet(resultSetId)).Select(Mapper.Map<ResultDto>).ToList();
        }

        public async Task<ResultSetDto> GetResultSetById(int resultSetId)
        {
            var resultSet = await UnitOfWork.ResultSets.GetById(resultSetId);

            if (resultSet == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Result set not found.");
            }

            return Mapper.Map<ResultSetDto>(resultSet);
        }

        public async Task<IEnumerable<ResultSetDto>> GetResultSetsByStudent(int studentId)
        {
            return (await UnitOfWork.ResultSets.GetResultSetsByStudent(studentId)).Select(Mapper.Map<ResultSetDto>).ToList();
        }

        public async Task UpdateResultSet(ResultSetDto resultSet)
        {
            var resultSetInDb = await UnitOfWork.ResultSets.GetById(resultSet.Id);

            if (resultSetInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Result set not found.");
            }

            resultSetInDb.Name = resultSet.Name;

            await UnitOfWork.Complete();
        }

        public async Task<IDictionary<int, string>> GetAllResultSetsLookup()
        {
            return (await UnitOfWork.ResultSets.GetAll()).ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
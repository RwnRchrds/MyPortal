using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Persistence;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/assessment")]
    public class AssessmentController : MyPortalApiController
    {
        private readonly AssessmentService _service;

        public AssessmentController()
        {
            _service = new AssessmentService(UnitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }

        [HttpPost]
        [RequiresPermission("EditResults")]
        [Route("results/create", Name = "ApiCreateResult")]
        public async Task<IHttpActionResult> CreateResult([FromBody] AssessmentResult result)
        {
            try
            {
                await _service.CreateResult(result);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result created");
        }

        [HttpGet]
        [RequiresPermission("ViewResults")]
        [Route("results/get/byStudent/{studentId:int}/{resultSetId:int}", Name = "ApiGetResultsByStudent")]
        public async Task<IEnumerable<AssessmentResultDto>> GetResultsByStudent([FromUri] int studentId, [FromUri] int resultSetId)
        {
            try
            {
                var results = await _service.GetResultsByStudent(studentId, resultSetId);

                return results.Select(Mapper.Map<AssessmentResult, AssessmentResultDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewResults")]
        [Route("results/get/byStudent/{studentId:int}/{resultsetId:int}", Name = "ApiGetResultsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetResultsByStudentDataGrid([FromUri] int studentId, [FromUri] int resultSetId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var results = await _service.GetResultsByStudent(studentId, resultSetId);

                var list = results.Select(Mapper.Map<AssessmentResult, GridAssessmentResultDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/create", Name = "ApiCreateResultSet")]
        public async Task<IHttpActionResult> CreateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            try
            {
                await _service.CreateResultSet(resultSet);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result set created");
        }

        [HttpDelete]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/delete/{resultSetId:int}", Name = "ApiDeleteResultSet")]
        public async Task<IHttpActionResult> DeleteResultSet([FromUri] int resultSetId)
        {
            try
            {
                await _service.DeleteResultSet(resultSetId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result set deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byId/{resultSetId:int}", Name = "ApiGetResultSetById")]
        public async Task<AssessmentResultSetDto> GetResultSetById([FromUri] int resultSetId)
        {
            try
            {
                var resultSet = await _service.GetResultSetById(resultSetId);

                return Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>(resultSet);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/all", Name = "ApiGetAllResultSets")]
        public async Task<IEnumerable<AssessmentResultSetDto>> GetAllResultSets()
        {
            try
            {
                var resultSets = await _service.GetAllResultSets();

                return resultSets.Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byStudent/{studentId:int}", Name = "ApiGetResultSetsByStudent")]
        public async Task<IEnumerable<AssessmentResultSetDto>> GetResultSetsByStudent([FromUri] int studentId)
        {
            try
            {
                var resultSets = await _service.GetResultSetsByStudent(studentId);

                return resultSets.Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/dataGrid/all", Name = "ApiGetAllResultSetsDataGrid")]
        public async Task<IHttpActionResult> GetAllResultSetsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var resultSets = await _service.GetAllResultSets();

                var list = resultSets.Select(Mapper.Map<AssessmentResultSet, GridAssessmentResultSetDto>);
                
                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/setCurrent/{resultSetId:int}", Name = "ApiSetResultSetAsCurrent")]
        public async Task<IHttpActionResult> SetResultSetAsCurrent([FromUri] int resultSetId)
        {
            try
            {
                await _service.SetResultSetAsCurrent(resultSetId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result set marked as current");
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/update", Name = "ApiUpdateResultSet")]
        public async Task<IHttpActionResult> UpdateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            try
            {
                await _service.UpdateResultSet(resultSet);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result set updated");
        }
    }
}

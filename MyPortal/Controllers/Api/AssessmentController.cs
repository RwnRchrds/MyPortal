using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Models.Database;
using MyPortal.Persistence;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/assessment")]
    public class AssessmentController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("EditResults")]
        [Route("results/create", Name = "ApiAssessmentCreateResult")]
        public async Task<IHttpActionResult> CreateResult([FromBody] AssessmentResult result)
        {
            try
            {
                using (var service = new AssessmentService(UnitOfWork))
                {
                    await service.CreateResult(result);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result created");
        }

        [HttpGet]
        [RequiresPermission("ViewResults")]
        [Route("results/get/byStudent/{studentId:int}/{resultSetId:int}", Name = "ApiAssessmentGetResultsByStudent")]
        public async Task<IEnumerable<AssessmentResultDto>> GetResultsByStudent([FromUri] int studentId, [FromUri] int resultSetId)
        {
            try
            {
                using (var service = new AssessmentService(UnitOfWork))
                {
                    return await service.GetResultsByStudentDto(studentId, resultSetId);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewResults")]
        [Route("results/get/byStudent/{studentId:int}/{resultsetId:int}", Name = "ApiAssessmentGetResultsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetResultsByStudentDataGrid([FromUri] int studentId, [FromUri] int resultSetId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                using (var service = new AssessmentService(UnitOfWork))
                {
                    var results = await service.GetResultsByStudentDataGrid(studentId, resultSetId);
                    return PrepareDataGridObject(results, dm);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditResults")]
        [Route("resultSets/create", Name = "ApiAssessmentCreateResultSet")]
        public async Task<IHttpActionResult> CreateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            try
            {
                using (var service = new AssessmentService(UnitOfWork))
                {
                    await service.CreateResultSet(resultSet);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result set created");
        }

        [HttpDelete]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/delete/{resultSetId:int}", Name = "ApiAssessmentDeleteResultSet")]
        public async Task<IHttpActionResult> DeleteResultSet([FromUri] int resultSetId)
        {
            try
            {
                using (var service = new AssessmentService(UnitOfWork))
                {
                    await service.DeleteResultSet(resultSetId);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result set deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byId/{resultSetId:int}", Name = "ApiAssessmentGetResultSetById")]
        public async Task<AssessmentResultSetDto> GetResultSetById([FromUri] int resultSetId)
        {
            try
            {
                using (var service = new AssessmentService(UnitOfWork))
                {
                    return await service.GetResultSetByIdDto(resultSetId);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/all", Name = "ApiAssessmentGetAllResultSets")]
        public async Task<IEnumerable<AssessmentResultSetDto>> GetAllResultSets()
        {
            try
            {
                using (var service = new AssessmentService(UnitOfWork))
                {
                    return await service.GetAllResultSetsDto();
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byStudent/{studentId:int}", Name = "ApiAssessmentGetResultSetsByStudent")]
        public async Task<IEnumerable<AssessmentResultSetDto>> GetResultSetsByStudent([FromUri] int studentId)
        {
            try
            {
                return await _service.GetResultSetsByStudent(studentId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/dataGrid/all", Name = "ApiAssessmentGetAllResultSetsDataGrid")]
        public async Task<IHttpActionResult> GetAllResultSetsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var resultSets = await _service.GetAllResultSetsDataGrid();
                return PrepareDataGridObject(resultSets, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/hasResults/{resultSetId:int}", Name = "ApiAssessmentResultSetHasResults")]
        public async Task<bool> ResultSetHasResults([FromUri] int resultSetId)
        {
            try
            {
                return await _service.ResultSetContainsResults(resultSetId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/setCurrent/{resultSetId:int}", Name = "ApiAssessmentSetResultSetAsCurrent")]
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
        [Route("resultSets/update", Name = "ApiAssessmentUpdateResultSet")]
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

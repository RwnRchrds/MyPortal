using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;
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
                await AssessmentProcesses.CreateResult(result, _context);
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
                return await AssessmentProcesses.GetResultsByStudent(studentId, resultSetId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
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
                var results = await AssessmentProcesses.GetResultsByStudentDataGrid(studentId, resultSetId, _context);
                return PrepareDataGridObject(results, dm);
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
                await AssessmentProcesses.CreateResultSet(resultSet, _context);
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
                await AssessmentProcesses.DeleteResultSet(resultSetId, _context);
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
                return await AssessmentProcesses.GetResultSetById(resultSetId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/all", Name = "ApiAssessmentGetAllResultSets")]
        public async Task<IEnumerable<AssessmentResultSetDto>> GetAllResultSets()
        {
            try
            {
                return await AssessmentProcesses.GetAllResultSets(_context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byStudent/{studentId:int}", Name = "ApiAssessmentGetResultSetsByStudent")]
        public async Task<IEnumerable<AssessmentResultSetDto>> GetResultSetsByStudent([FromUri] int studentId)
        {
            try
            {
                return await AssessmentProcesses.GetResultSetsByStudent(studentId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/dataGrid/all", Name = "ApiAssessmentGetAllResultSetsDataGrid")]
        public async Task<IHttpActionResult> GetAllResultSetsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var resultSets = await AssessmentProcesses.GetAllResultSetsDataGrid(_context);
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
                return await AssessmentProcesses.ResultSetContainsResults(resultSetId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return false;
            }
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/setCurrent/{resultSetId:int}", Name = "ApiAssessmentSetResultSetAsCurrent")]
        public async Task<IHttpActionResult> SetResultSetAsCurrent([FromUri] int resultSetId)
        {
            try
            {
                await AssessmentProcesses.SetResultSetAsCurrent(resultSetId, _context);
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
                await AssessmentProcesses.UpdateResultSet(resultSet, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Result set updated");
        }
    }
}

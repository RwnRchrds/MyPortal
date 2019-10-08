using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
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
        public IHttpActionResult CreateResult([FromBody] AssessmentResult result)
        {
            try
            {
                AssessmentProcesses.CreateResult(result, _context);
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Result created");
        }

        [HttpGet]
        [RequiresPermission("ViewResults")]
        [Route("results/get/byStudent/{studentId:int}/{resultSetId:int}", Name = "ApiAssessmentGetResultsByStudent")]
        public IEnumerable<AssessmentResultDto> GetResultsByStudent([FromUri] int studentId, [FromUri] int resultSetId)
        {
            return PrepareResponseObject(AssessmentProcesses.GetResultsByStudent(studentId, resultSetId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewResults")]
        [Route("results/get/byStudent/{studentId:int}/{resultsetId:int}", Name = "ApiAssessmentGetResultsByStudentDataGrid")]
        public IHttpActionResult GetResultsByStudentDataGrid([FromUri] int studentId, [FromUri] int resultSetId,
            [FromBody] DataManagerRequest dm)
        {
            var results =
                PrepareResponseObject(
                    AssessmentProcesses.GetResultsByStudentDataGrid(studentId, resultSetId, _context));

            return PrepareDataGridObject(results, dm);
        }

        [HttpPost]
        [RequiresPermission("EditResults")]
        [Route("resultSets/create", Name = "ApiAssessmentCreateResultSet")]
        public IHttpActionResult CreateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            return PrepareResponse(AssessmentProcesses.CreateResultSet(resultSet, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/delete/{resultSetId:int}", Name = "ApiAssessmentDeleteResultSet")]
        public IHttpActionResult DeleteResultSet([FromUri] int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.DeleteResultSet(resultSetId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byId/{resultSetId:int}", Name = "ApiAssessmentGetResultSetById")]
        public AssessmentResultSetDto GetResultSetById([FromUri] int resultSetId)
        {
            return PrepareResponseObject(
                AssessmentProcesses.GetResultSetById(resultSetId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/all", Name = "ApiAssessmentGetAllResultSets")]
        public IEnumerable<AssessmentResultSetDto> GetAllResultSets()
        {
            return PrepareResponseObject(AssessmentProcesses.GetAllResultSets(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byStudent/{studentId:int}", Name = "ApiAssessmentGetResultSetsByStudent")]
        public IEnumerable<AssessmentResultSetDto> GetResultSetsByStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(AssessmentProcesses.GetResultSetsByStudent(studentId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/dataGrid/all", Name = "ApiAssessmentGetAllResultSetsDataGrid")]
        public IHttpActionResult GetAllResultSetsDataGrid([FromBody] DataManagerRequest dm)
        {
            var resultSets = PrepareResponseObject(AssessmentProcesses.GetAllResultSets_DataGrid(_context));

            return PrepareDataGridObject(resultSets, dm);
        }

        [HttpGet]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/hasResults/{resultSetId:int}", Name = "ApiAssessmentResultSetHasResults")]
        public bool ResultSetHasResults([FromUri] int resultSetId)
        {
            return PrepareResponseObject(AssessmentProcesses.ResultSetContainsResults(resultSetId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/setCurrent/{resultSetId:int}", Name = "ApiAssessmentSetResultSetAsCurrent")]
        public IHttpActionResult SetResultSetAsCurrent([FromUri] int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.SetResultSetAsCurrent(resultSetId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/update", Name = "ApiAssessmentUpdateResultSet")]
        public IHttpActionResult UpdateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            return PrepareResponse(AssessmentProcesses.UpdateResultSet(resultSet, _context));
        }
    }
}

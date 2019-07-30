using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/assessment")]
    public class AssessmentController : MyPortalApiController
    {
        [HttpGet]
        [Route("results/import/{resultSetId:int}")]
        public IHttpActionResult ImportResults([FromUri] int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.ImportResultsToResultSet(resultSetId, _context));
        }

        [HttpPost]
        [Route("results/create")]
        public IHttpActionResult AddResult([FromBody] AssessmentResult result)
        {
            return PrepareResponse(AssessmentProcesses.CreateResult(result, _context));
        }

        [HttpGet]
        [Route("results/get/{studentId:int}/{resultSetId:int}")]
        public IEnumerable<AssessmentResultDto> GetResults([FromUri] int studentId, [FromUri] int resultSetId)
        {
            return PrepareResponseObject(AssessmentProcesses.GetResultsForStudent(studentId, resultSetId, _context));
        }

        [HttpPost]
        [Route("resultSets/create")]
        public IHttpActionResult CreateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            return PrepareResponse(AssessmentProcesses.CreateResultSet(resultSet, _context));
        }

        [HttpDelete]
        [Route("resultSets/delete/{resultSetId:int}")]
        public IHttpActionResult DeleteResultSet([FromUri] int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.DeleteResultSet(resultSetId, _context));
        }

        [HttpGet]
        [Route("resultSets/get/byId/{resultSetId:int}")]
        public AssessmentResultSetDto GetResultSet([FromUri] int resultSetId)
        {
            return PrepareResponseObject(
                AssessmentProcesses.GetResultSetById(resultSetId, _context));
        }

        [HttpGet]
        [Route("resultSets/get/all")]
        public IEnumerable<AssessmentResultSetDto> GetAllResultSets()
        {
            return PrepareResponseObject(AssessmentProcesses.GetAllResultSets(_context));
        }

        [HttpPost]
        [Route("resultSets/get/dataGrid/all")]
        public IHttpActionResult GetAllResultSetsForDataGrid([FromBody] DataManagerRequest dm)
        {
            var resultSets = PrepareResponseObject(AssessmentProcesses.GetAllResultSets_DataGrid(_context));

            return PrepareDataGridObject(resultSets, dm);
        }

        [HttpGet]
        [Route("resultSets/hasResults/{resultSetId:int}")]
        public bool ResultSetHasResults([FromUri] int resultSetId)
        {
            return PrepareResponseObject(AssessmentProcesses.ResultSetContainsResults(resultSetId, _context));
        }


        [HttpPost]
        [Route("resultSets/setCurrent/{resultSetId:int}")]
        public IHttpActionResult SetCurrent([FromUri] int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.SetResultSetAsCurrent(resultSetId, _context));
        }

        [HttpPost]
        [Route("resultSets/update")]
        public IHttpActionResult UpdateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            return PrepareResponse(AssessmentProcesses.UpdateResultSet(resultSet, _context));
        }
    }
}

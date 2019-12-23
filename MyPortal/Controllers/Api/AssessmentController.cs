using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/assessment")]
    public class AssessmentController : MyPortalApiController
    {
        private readonly AssessmentService _service;

        public AssessmentController()
        {
            _service = new AssessmentService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }

        [HttpPost]
        [RequiresPermission("EditResults")]
        [Route("results/create", Name = "ApiCreateResult")]
        public async Task<IHttpActionResult> CreateResult([FromBody] ResultDto result)
        {
            try
            {
                await _service.CreateResult(result);

                return Ok("Result created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResults")]
        [Route("results/get/byStudent/{studentId:int}/{resultSetId:int}", Name = "ApiGetResultsByStudent")]
        public async Task<IEnumerable<ResultDto>> GetResultsByStudent([FromUri] int studentId, [FromUri] int resultSetId)
        {
            try
            {
                return await _service.GetResultsByStudent(studentId, resultSetId);
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

                var list = results.Select(_mapping.Map<DataGridResultDto>);

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
        public async Task<IHttpActionResult> CreateResultSet([FromBody] ResultSetDto resultSet)
        {
            try
            {
                await _service.CreateResultSet(resultSet);

                return Ok("Result set created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/delete/{resultSetId:int}", Name = "ApiDeleteResultSet")]
        public async Task<IHttpActionResult> DeleteResultSet([FromUri] int resultSetId)
        {
            try
            {
                await _service.DeleteResultSet(resultSetId);

                return Ok("Result set deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byId/{resultSetId:int}", Name = "ApiGetResultSetById")]
        public async Task<ResultSetDto> GetResultSetById([FromUri] int resultSetId)
        {
            try
            {
                return await _service.GetResultSetById(resultSetId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/all", Name = "ApiGetAllResultSets")]
        public async Task<IEnumerable<ResultSetDto>> GetAllResultSets()
        {
            try
            {
                return await _service.GetAllResultSets();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewResultSets")]
        [Route("resultSets/get/byStudent/{studentId:int}", Name = "ApiGetResultSetsByStudent")]
        public async Task<IEnumerable<ResultSetDto>> GetResultSetsByStudent([FromUri] int studentId)
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
        [Route("resultSets/get/dataGrid/all", Name = "ApiGetAllResultSetsDataGrid")]
        public async Task<IHttpActionResult> GetAllResultSetsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var list = (await _service.GetAllResultSets()).Select(_mapping.Map<DataGridResultSetDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditResultSets")]
        [Route("resultSets/update", Name = "ApiUpdateResultSet")]
        public async Task<IHttpActionResult> UpdateResultSet([FromBody] ResultSetDto resultSet)
        {
            try
            {
                await _service.UpdateResultSet(resultSet);

                return Ok("Result set updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

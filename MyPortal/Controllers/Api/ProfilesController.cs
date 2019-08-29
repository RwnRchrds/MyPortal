using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/profiles")]
    public class ProfilesController : MyPortalApiController
    {
        [HttpPost]
        [Route("logs/create")]
        public IHttpActionResult CreateLog([FromBody] ProfileLog log)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var userId = User.Identity.GetUserId();

            return PrepareResponse(ProfilesProcesses.CreateLog(log, academicYearId, userId, _context));
        }
        
        [Route("logs/delete/{logId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteLog([FromUri] int logId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteLog(logId, _context));
        }

        [HttpGet]
        [Route("logs/get/byId/{logId:int}")]
        public ProfileLogDto GetLogById([FromUri] int logId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetLogById(logId, _context));
        }
        
        [HttpGet]
        [Route("logs/get/byStudent/{studentId:int}")]
        public IEnumerable<ProfileLogDto> GetLogsForStudent([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(ProfilesProcesses.GetLogsForStudent(studentId, academicYearId, _context));
        }

        [HttpPost]
        [Route("logs/get/byStudent/dataGrid/{studentId:int}")]
        public IHttpActionResult GetLogsForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var logs = PrepareResponseObject(
                ProfilesProcesses.GetLogsForStudent_DataGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(logs, dm);
        }

        [Route("logs/update")]
        [HttpPost]
        public IHttpActionResult UpdateLog([FromBody] ProfileLog log)
        {
            return PrepareResponse(ProfilesProcesses.UpdateLog(log, _context));
        }

        [HttpGet]
        [System.Web.Mvc.Route("commentBanks/hasComments/{bankId:int}")]
        public bool CommentBankHasComments([FromUri] int bankId)
        {
            return PrepareResponseObject(ProfilesProcesses.CommentBankContainsComments(bankId, _context));
        }
        
        [HttpPost]
        [Route("commentBanks/create")]
        public IHttpActionResult CreateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesProcesses.CreateCommentBank(commentBank, _context));
        }

        [HttpDelete]
        [Route("commentBanks/delete/{commentBankId:int}")]
        public IHttpActionResult DeleteCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteCommentBank(commentBankId, _context));
        }

        [HttpGet]
        [Route("commentBanks/get/byId/{commentBankId:int}")]
        public ProfileCommentBankDto GetCommentBankById([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentBankById(commentBankId, _context));
        }

        [HttpGet]
        [Route("commentBanks/get/all")]
        public IEnumerable<ProfileCommentBankDto> GetAllCommentBanks()
        {
            return PrepareResponseObject(ProfilesProcesses.GetAllCommentBanks(_context));
        }

        [HttpPost]
        [Route("commentBanks/get/dataGrid/all")]
        public IHttpActionResult GetAllCommentBanksForDataGrid([FromBody] DataManagerRequest dm)
        {
            var commentBanks = PrepareResponseObject(ProfilesProcesses.GetAllCommentBanks_Model(_context));

            return PrepareDataGridObject(commentBanks, dm);
        }

        [HttpPost]
        [Route("commentBanks/update")]
        public IHttpActionResult UpdateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesProcesses.UpdateCommentBank(commentBank, _context));
        }

        [HttpPost]
        [Route("comments/create")]
        public IHttpActionResult CreateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesProcesses.CreateComment(comment, _context));
        }

        [HttpDelete]
        [Route("comments/delete/{commentId:int}")]
        public IHttpActionResult DeleteComment(int commentId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteComment(commentId, _context));
        }

        [HttpGet]
        [Route("comments/get/byId/{commentId:int}")]
        public ProfileCommentDto GetCommentById([FromUri] int commentId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentById(commentId, _context));
        }

        [HttpGet]
        [Route("comments/get/all")]
        public IEnumerable<ProfileCommentDto> GetComments()
        {
            return PrepareResponseObject(ProfilesProcesses.GetAllComments(_context));
        }

        [HttpGet]
        [Route("comments/get/byBank/{commentBankId:int}")]
        public IEnumerable<ProfileCommentDto> GetCommentsByCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentsByBank(commentBankId, _context));
        }

        [HttpPost]
        [Route("comments/get/byBank/dataGrid/{commentBankId:int}")]
        public IHttpActionResult GetCommentsByCommentBankForDataGrid([FromUri] int commentBankId,
            [FromBody] DataManagerRequest dm)
        {
            var comments = PrepareResponseObject(ProfilesProcesses.GetCommentsByBank_DataGrid(commentBankId, _context));

            return PrepareDataGridObject(comments, dm);
        }

        [HttpPost]
        [Route("comments/update")]
        public IHttpActionResult UpdateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesProcesses.UpdateComment(comment, _context));
        }
    }
}
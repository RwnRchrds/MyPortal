using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/profiles")]
    [Authorize]
    public class ProfilesController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("EditProfileLogs")]
        [Route("logs/create", Name = "ApiProfilesCreateLog")]
        public IHttpActionResult CreateLog([FromBody] ProfileLog log)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var userId = User.Identity.GetUserId();

            return PrepareResponse(ProfilesProcesses.CreateLog(log, academicYearId, userId, _context));
        }
        
        [Route("logs/delete/{logId:int}", Name = "ApiProfilesDeleteLog")]
        [RequiresPermission("EditProfileLogs")]
        [HttpDelete]
        public IHttpActionResult DeleteLog([FromUri] int logId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteLog(logId, _context));
        }

        [HttpGet]
        [Route("logs/get/byId/{logId:int}", Name = "ApiProfilesGetLogById")]
        [RequiresPermission("ViewProfileLogs")]
        public ProfileLogDto GetLogById([FromUri] int logId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetLogById(logId, _context));
        }
        
        [HttpGet]
        [Route("logs/get/byStudent/{studentId:int}", Name = "ApiProfilesGetLogsByStudent")]
        [RequiresPermission("ViewProfileLogs")]
        public IEnumerable<ProfileLogDto> GetLogsByStudent([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(ProfilesProcesses.GetLogsByStudent(studentId, academicYearId, _context));
        }

        [HttpPost]
        [Route("logs/get/byStudent/dataGrid/{studentId:int}", Name = "ApiProfilesGetLogsByStudentDataGrid")]
        [RequiresPermission("ViewProfileLogs")]
        public IHttpActionResult GetLogsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var logs = PrepareResponseObject(
                ProfilesProcesses.GetLogsByStudent_DataGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(logs, dm);
        }

        [Route("logs/update", Name = "ApiProfilesUpdateLog")]
        [RequiresPermission("EditProfileLogs")]
        [HttpPost]
        public IHttpActionResult UpdateLog([FromBody] ProfileLog log)
        {
            return PrepareResponse(ProfilesProcesses.UpdateLog(log, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/hasComments/{bankId:int}", Name = "ApiProfilesCommentBankHasComments")]
        public bool CommentBankHasComments([FromUri] int bankId)
        {
            return PrepareResponseObject(ProfilesProcesses.CommentBankContainsComments(bankId, _context));
        }
        
        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/create", Name = "ApiProfilesCreateCommentBank")]
        public IHttpActionResult CreateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesProcesses.CreateCommentBank(commentBank, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/delete/{commentBankId:int}", Name = "ApiProfilesDeleteCommentBank")]
        public IHttpActionResult DeleteCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteCommentBank(commentBankId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/byId/{commentBankId:int}", Name = "ApiProfilesGetCommentBankById")]
        public ProfileCommentBankDto GetCommentBankById([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentBankById(commentBankId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/all", Name = "ApiProfilesGetAllCommentBanks")]
        public IEnumerable<ProfileCommentBankDto> GetAllCommentBanks()
        {
            return PrepareResponseObject(ProfilesProcesses.GetAllCommentBanks(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/dataGrid/all", Name = "ApiProfilesGetAllCommentBanksDataGrid")]
        public IHttpActionResult GetAllCommentBanksDataGrid([FromBody] DataManagerRequest dm)
        {
            var commentBanks = PrepareResponseObject(ProfilesProcesses.GetAllCommentBanks_DataGrid(_context));

            return PrepareDataGridObject(commentBanks, dm);
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/update", Name = "ApiProfilesUpdateCommentBank")]
        public IHttpActionResult UpdateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesProcesses.UpdateCommentBank(commentBank, _context));
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/create", Name = "ApiProfilesCreateComment")]
        public IHttpActionResult CreateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesProcesses.CreateComment(comment, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("comments/delete/{commentId:int}", Name = "ApiProfilesDeleteComment")]
        public IHttpActionResult DeleteComment(int commentId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteComment(commentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byId/{commentId:int}", Name = "ApiProfilesGetCommentById")]
        public ProfileCommentDto GetCommentById([FromUri] int commentId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentById(commentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/all", Name = "ApiProfilesGetAllComments")]
        public IEnumerable<ProfileCommentDto> GetAllComments()
        {
            return PrepareResponseObject(ProfilesProcesses.GetAllComments(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/{commentBankId:int}", Name = "ApiProfilesGetCommentsByCommentBank")]
        public IEnumerable<ProfileCommentDto> GetCommentsByCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentsByBank(commentBankId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/dataGrid/{commentBankId:int}", Name = "ApiProfilesGetCommentsByCommentBankDataGrid")]
        public IHttpActionResult GetCommentsByCommentBankDataGrid([FromUri] int commentBankId,
            [FromBody] DataManagerRequest dm)
        {
            var comments = PrepareResponseObject(ProfilesProcesses.GetCommentsByBank_DataGrid(commentBankId, _context));

            return PrepareDataGridObject(comments, dm);
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/update", Name = "ApiProfilesUpdateComment")]
        public IHttpActionResult UpdateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesProcesses.UpdateComment(comment, _context));
        }
    }
}
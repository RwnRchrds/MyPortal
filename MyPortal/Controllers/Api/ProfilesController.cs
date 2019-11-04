using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Database;
using MyPortal.Services;
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
        public async Task<IHttpActionResult> CreateLog([FromBody] ProfileLog log)
        {
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);
            var userId = User.Identity.GetUserId();

            return PrepareResponse(ProfilesService.CreateLog(log, academicYearId, userId, _context));
        }
        
        [Route("logs/delete/{logId:int}", Name = "ApiProfilesDeleteLog")]
        [RequiresPermission("EditProfileLogs")]
        [HttpDelete]
        public IHttpActionResult DeleteLog([FromUri] int logId)
        {
            return PrepareResponse(ProfilesService.DeleteLog(logId, _context));
        }

        [HttpGet]
        [Route("logs/get/byId/{logId:int}", Name = "ApiProfilesGetLogById")]
        [RequiresPermission("ViewProfileLogs")]
        public ProfileLogDto GetLogById([FromUri] int logId)
        {
            return PrepareResponseObject(ProfilesService.GetLogById(logId, _context));
        }
        
        [HttpGet]
        [Route("logs/get/byStudent/{studentId:int}", Name = "ApiProfilesGetLogsByStudent")]
        [RequiresPermission("ViewProfileLogs")]
        public async Task<IEnumerable<ProfileLogDto>> GetLogsByStudent([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(ProfilesService.GetLogsByStudent(studentId, academicYearId, _context));
        }

        [HttpPost]
        [Route("logs/get/byStudent/dataGrid/{studentId:int}", Name = "ApiProfilesGetLogsByStudentDataGrid")]
        [RequiresPermission("ViewProfileLogs, AccessStudentPortal")]
        public async Task<IHttpActionResult> GetLogsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);
            var logs = PrepareResponseObject(
                ProfilesService.GetLogsByStudent_DataGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(logs, dm);
        }

        [Route("logs/update", Name = "ApiProfilesUpdateLog")]
        [RequiresPermission("EditProfileLogs")]
        [HttpPost]
        public IHttpActionResult UpdateLog([FromBody] ProfileLog log)
        {
            return PrepareResponse(ProfilesService.UpdateLog(log, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/hasComments/{bankId:int}", Name = "ApiProfilesCommentBankHasComments")]
        public bool CommentBankHasComments([FromUri] int bankId)
        {
            return PrepareResponseObject(ProfilesService.CommentBankHasComments(bankId, _context));
        }
        
        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/create", Name = "ApiProfilesCreateCommentBank")]
        public IHttpActionResult CreateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesService.CreateCommentBank(commentBank, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/delete/{commentBankId:int}", Name = "ApiProfilesDeleteCommentBank")]
        public IHttpActionResult DeleteCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponse(ProfilesService.DeleteCommentBank(commentBankId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/byId/{commentBankId:int}", Name = "ApiProfilesGetCommentBankById")]
        public ProfileCommentBankDto GetCommentBankById([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesService.GetCommentBankById(commentBankId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/all", Name = "ApiProfilesGetAllCommentBanks")]
        public IEnumerable<ProfileCommentBankDto> GetAllCommentBanks()
        {
            return PrepareResponseObject(ProfilesService.GetAllCommentBanks(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/dataGrid/all", Name = "ApiProfilesGetAllCommentBanksDataGrid")]
        public IHttpActionResult GetAllCommentBanksDataGrid([FromBody] DataManagerRequest dm)
        {
            var commentBanks = PrepareResponseObject(ProfilesService.GetAllCommentBanksDataGrid(_context));

            return PrepareDataGridObject(commentBanks, dm);
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/update", Name = "ApiProfilesUpdateCommentBank")]
        public IHttpActionResult UpdateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesService.UpdateCommentBank(commentBank, _context));
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/create", Name = "ApiProfilesCreateComment")]
        public IHttpActionResult CreateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesService.CreateComment(comment, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("comments/delete/{commentId:int}", Name = "ApiProfilesDeleteComment")]
        public IHttpActionResult DeleteComment(int commentId)
        {
            return PrepareResponse(ProfilesService.DeleteComment(commentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byId/{commentId:int}", Name = "ApiProfilesGetCommentById")]
        public ProfileCommentDto GetCommentById([FromUri] int commentId)
        {
            return PrepareResponseObject(ProfilesService.GetCommentById(commentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/all", Name = "ApiProfilesGetAllComments")]
        public IEnumerable<ProfileCommentDto> GetAllComments()
        {
            return PrepareResponseObject(ProfilesService.GetAllComments(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/{commentBankId:int}", Name = "ApiProfilesGetCommentsByCommentBank")]
        public IEnumerable<ProfileCommentDto> GetCommentsByCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesService.GetCommentsByBank(commentBankId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/dataGrid/{commentBankId:int}", Name = "ApiProfilesGetCommentsByCommentBankDataGrid")]
        public IHttpActionResult GetCommentsByCommentBankDataGrid([FromUri] int commentBankId,
            [FromBody] DataManagerRequest dm)
        {
            var comments = PrepareResponseObject(ProfilesService.GetCommentsByBank_DataGrid(commentBankId, _context));

            return PrepareDataGridObject(comments, dm);
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/update", Name = "ApiProfilesUpdateComment")]
        public IHttpActionResult UpdateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesService.UpdateComment(comment, _context));
        }
    }
}
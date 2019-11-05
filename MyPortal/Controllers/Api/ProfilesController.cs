using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
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
        private readonly ProfilesService _service;

        public ProfilesController()
        {
            _service = new ProfilesService(UnitOfWork);
        }
        
        [HttpPost]
        [RequiresPermission("EditProfileLogs")]
        [Route("logs/create", Name = "ApiProfilesCreateLog")]
        public async Task<IHttpActionResult> CreateLog([FromBody] ProfileLog log)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);
                    var userId = User.Identity.GetUserId();

                    await _service.CreateLog(log, academicYearId, userId);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Log created");
        }
        
        [Route("logs/delete/{logId:int}", Name = "ApiProfilesDeleteLog")]
        [RequiresPermission("EditProfileLogs")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteLog([FromUri] int logId)
        {
            try
            {
                await _service.DeleteLog(logId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Log deleted");
        }

        [HttpGet]
        [Route("logs/get/byId/{logId:int}", Name = "ApiProfilesGetLogById")]
        [RequiresPermission("ViewProfileLogs")]
        public async Task<ProfileLogDto> GetLogById([FromUri] int logId)
        {
            try
            {
                var log = await _service.GetLogById(logId);

                return Mapper.Map<ProfileLog, ProfileLogDto>(log);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
        
        [HttpGet]
        [Route("logs/get/byStudent/{studentId:int}", Name = "ApiProfilesGetLogsByStudent")]
        [RequiresPermission("ViewProfileLogs")]
        public async Task<IEnumerable<ProfileLogDto>> GetLogsByStudent([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var logs = await _service.GetLogsByStudent(studentId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
        public async Task<IHttpActionResult> UpdateLog([FromBody] ProfileLog log)
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
        public async Task<IHttpActionResult> CreateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesService.CreateCommentBank(commentBank, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/delete/{commentBankId:int}", Name = "ApiProfilesDeleteCommentBank")]
        public async Task<IHttpActionResult> DeleteCommentBank([FromUri] int commentBankId)
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
        public async Task<IHttpActionResult> GetAllCommentBanksDataGrid([FromBody] DataManagerRequest dm)
        {
            var commentBanks = PrepareResponseObject(ProfilesService.GetAllCommentBanksDataGrid(_context));

            return PrepareDataGridObject(commentBanks, dm);
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/update", Name = "ApiProfilesUpdateCommentBank")]
        public async Task<IHttpActionResult> UpdateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesService.UpdateCommentBank(commentBank, _context));
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/create", Name = "ApiProfilesCreateComment")]
        public async Task<IHttpActionResult> CreateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesService.CreateComment(comment, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("comments/delete/{commentId:int}", Name = "ApiProfilesDeleteComment")]
        public async Task<IHttpActionResult> DeleteComment(int commentId)
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
        public async Task<IHttpActionResult> GetCommentsByCommentBankDataGrid([FromUri] int commentBankId,
            [FromBody] DataManagerRequest dm)
        {
            var comments = PrepareResponseObject(ProfilesService.GetCommentsByBank_DataGrid(commentBankId, _context));

            return PrepareDataGridObject(comments, dm);
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/update", Name = "ApiProfilesUpdateComment")]
        public async Task<IHttpActionResult> UpdateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesService.UpdateComment(comment, _context));
        }
    }
}
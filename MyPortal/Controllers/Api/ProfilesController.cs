using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.GridDtos;
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

                    var logs = await _service.GetLogsByStudent(studentId, academicYearId);

                    return logs.Select(Mapper.Map<ProfileLog, ProfileLogDto>);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [Route("logs/get/byStudent/dataGrid/{studentId:int}", Name = "ApiProfilesGetLogsByStudentDataGrid")]
        [RequiresPermission("ViewProfileLogs, AccessStudentPortal")]
        public async Task<IHttpActionResult> GetLogsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var logs = await _service.GetLogsByStudent(studentId, academicYearId);

                    var list = logs.Select(Mapper.Map<ProfileLog, GridProfileLogDto>);

                    return PrepareDataGridObject(list, dm);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [Route("logs/update", Name = "ApiProfilesUpdateLog")]
        [RequiresPermission("EditProfileLogs")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateLog([FromBody] ProfileLog log)
        {
            try
            {
                await _service.UpdateLog(log);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Log updated");
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/create", Name = "ApiProfilesCreateCommentBank")]
        public async Task<IHttpActionResult> CreateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            try
            {
                await _service.CreateCommentBank(commentBank);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Comment bank created");
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/delete/{commentBankId:int}", Name = "ApiProfilesDeleteCommentBank")]
        public async Task<IHttpActionResult> DeleteCommentBank([FromUri] int commentBankId)
        {
            try
            {
                await _service.DeleteCommentBank(commentBankId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Comment bank deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/byId/{commentBankId:int}", Name = "ApiProfilesGetCommentBankById")]
        public async Task<ProfileCommentBankDto> GetCommentBankById([FromUri] int commentBankId)
        {
            try
            {
                var commentBank = await _service.GetCommentBankById(commentBankId);

                return Mapper.Map<ProfileCommentBank, ProfileCommentBankDto>(commentBank);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/all", Name = "ApiProfilesGetAllCommentBanks")]
        public async Task<IEnumerable<ProfileCommentBankDto>> GetAllCommentBanks()
        {
            try
            {
                var commentBanks = await _service.GetAllCommentBanks();

                return commentBanks.Select(Mapper.Map<ProfileCommentBank, ProfileCommentBankDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/dataGrid/all", Name = "ApiProfilesGetAllCommentBanksDataGrid")]
        public async Task<IHttpActionResult> GetAllCommentBanksDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var commentBanks = await _service.GetAllCommentBanks();

                var list = commentBanks.Select(Mapper.Map<ProfileCommentBank, GridProfileCommentBankDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/update", Name = "ApiProfilesUpdateCommentBank")]
        public async Task<IHttpActionResult> UpdateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            try
            {
                await _service.UpdateCommentBank(commentBank);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Comment bank updated");
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/create", Name = "ApiProfilesCreateComment")]
        public async Task<IHttpActionResult> CreateComment([FromBody] ProfileComment comment)
        {
            try
            {
                await _service.CreateComment(comment);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Comment created");
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("comments/delete/{commentId:int}", Name = "ApiProfilesDeleteComment")]
        public async Task<IHttpActionResult> DeleteComment(int commentId)
        {
            try
            {
                await _service.DeleteComment(commentId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Comment deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byId/{commentId:int}", Name = "ApiProfilesGetCommentById")]
        public async Task<ProfileCommentDto> GetCommentById([FromUri] int commentId)
        {
            try
            {
                var comment = await _service.GetCommentById(commentId);

                return Mapper.Map<ProfileComment, ProfileCommentDto>(comment);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/all", Name = "ApiProfilesGetAllComments")]
        public async Task<IEnumerable<ProfileCommentDto>> GetAllComments()
        {
            try
            {
                var comments = await _service.GetAllComments();

                return comments.Select(Mapper.Map<ProfileComment, ProfileCommentDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/{commentBankId:int}", Name = "ApiProfilesGetCommentsByCommentBank")]
        public async Task<IEnumerable<ProfileCommentDto>> GetCommentsByCommentBank([FromUri] int commentBankId)
        {
            try
            {
                var comments = await _service.GetCommentsByBank(commentBankId);

                return comments.Select(Mapper.Map<ProfileComment, ProfileCommentDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/dataGrid/{commentBankId:int}", Name = "ApiProfilesGetCommentsByCommentBankDataGrid")]
        public async Task<IHttpActionResult> GetCommentsByCommentBankDataGrid([FromUri] int commentBankId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var comments = await _service.GetCommentsByBank(commentBankId);

                var list = comments.Select(Mapper.Map<ProfileComment, GridProfileCommentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/update", Name = "ApiProfilesUpdateComment")]
        public async Task<IHttpActionResult> UpdateComment([FromBody] ProfileComment comment)
        {
            try
            {
                await _service.UpdateComment(comment);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Comment updated");
        }
    }
}
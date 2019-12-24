using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
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
            _service = new ProfilesService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpPost]
        [RequiresPermission("EditProfileLogNotes")]
        [Route("logNotes/create", Name = "ApiCreateProfileLogNote")]
        public async Task<IHttpActionResult> CreateLog([FromBody] ProfileLogNoteDto log)
        {
            try
            {
                using (var staffMemberService = new StaffMemberService())
                {
                    var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();
                    var userId = User.Identity.GetUserId();

                    var author = await staffMemberService.GetStaffMemberByUserId(userId);

                    log.AcademicYearId = academicYearId;
                    log.AuthorId = author.Id;

                    await _service.CreateLogNote(log);

                    return Ok("Log created");
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [Route("logNotes/delete/{logId:int}", Name = "ApiDeleteProfileLogNote")]
        [RequiresPermission("EditProfileLogNotes")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteLog([FromUri] int logId)
        {
            try
            {
                await _service.DeleteLogNote(logId);
                
                return Ok("Log deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("logNotes/get/byId/{logId:int}", Name = "ApiGetProfileLogNoteById")]
        [RequiresPermission("ViewProfileLogNotes")]
        public async Task<ProfileLogNoteDto> GetLogNoteById([FromUri] int logId)
        {
            try
            {
                return await _service.GetLogNoteById(logId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
        
        [HttpGet]
        [Route("logNotes/get/byStudent/{studentId:int}", Name = "ApiGetProfileLogNotesByStudent")]
        [RequiresPermission("ViewProfileLogNotes")]
        public async Task<IEnumerable<ProfileLogNoteDto>> GetLogsByStudent([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                return await _service.GetLogNotesByStudent(studentId, academicYearId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [Route("logNotes/get/byStudent/dataGrid/{studentId:int}", Name = "ApiGetProfileLogNotesByStudentDataGrid")]
        [RequiresPermission("ViewProfileLogNotes")]
        public async Task<IHttpActionResult> GetLogsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var logs = await _service.GetLogNotesByStudent(studentId, academicYearId);

                var list = logs.Select(_mapping.Map<DataGridProfileLogNoteDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [Route("logNotes/update", Name = "ApiUpdateProfileLogNote")]
        [RequiresPermission("EditProfileLogNotes")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateLog([FromBody] ProfileLogNoteDto log)
        {
            try
            {
                await _service.UpdateLogNote(log);

                return Ok("Log updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/create", Name = "ApiCreateCommentBank")]
        public async Task<IHttpActionResult> CreateCommentBank([FromBody] CommentBankDto commentBank)
        {
            try
            {
                await _service.CreateCommentBank(commentBank);

                return Ok("Comment bank created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/delete/{commentBankId:int}", Name = "ApiDeleteCommentBank")]
        public async Task<IHttpActionResult> DeleteCommentBank([FromUri] int commentBankId)
        {
            try
            {
                await _service.DeleteCommentBank(commentBankId);

                return Ok("Comment bank deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/byId/{commentBankId:int}", Name = "ApiGetCommentBankById")]
        public async Task<CommentBankDto> GetCommentBankById([FromUri] int commentBankId)
        {
            try
            {
                return await _service.GetCommentBankById(commentBankId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/all", Name = "ApiGetAllCommentBanks")]
        public async Task<IEnumerable<CommentBankDto>> GetAllCommentBanks()
        {
            try
            {
                return await _service.GetAllCommentBanks();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("commentBanks/get/dataGrid/all", Name = "ApiGetAllCommentBanksDataGrid")]
        public async Task<IHttpActionResult> GetAllCommentBanksDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var commentBanks = await _service.GetAllCommentBanks();

                var list = commentBanks.Select(_mapping.Map<DataGridCommentBankDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("commentBanks/update", Name = "ApiUpdateCommentBank")]
        public async Task<IHttpActionResult> UpdateCommentBank([FromBody] CommentBankDto commentBank)
        {
            try
            {
                await _service.UpdateCommentBank(commentBank);

                return Ok("Comment bank updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/create", Name = "ApiCreateComment")]
        public async Task<IHttpActionResult> CreateComment([FromBody] CommentDto comment)
        {
            try
            {
                await _service.CreateComment(comment);
                
                return Ok("Comment created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditComments")]
        [Route("comments/delete/{commentId:int}", Name = "ApiDeleteComment")]
        public async Task<IHttpActionResult> DeleteComment(int commentId)
        {
            try
            {
                await _service.DeleteComment(commentId);

                return Ok("Comment deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byId/{commentId:int}", Name = "ApiGetCommentById")]
        public async Task<CommentDto> GetCommentById([FromUri] int commentId)
        {
            try
            {
                return await _service.GetCommentById(commentId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/all", Name = "ApiGetAllComments")]
        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
            try
            {
                return await _service.GetAllComments();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/{commentBankId:int}", Name = "ApiGetCommentsByCommentBank")]
        public async Task<IEnumerable<CommentDto>> GetCommentsByCommentBank([FromUri] int commentBankId)
        {
            try
            {
                return await _service.GetCommentsByBank(commentBankId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewComments")]
        [Route("comments/get/byBank/dataGrid/{commentBankId:int}", Name = "ApiGetCommentsByCommentBankDataGrid")]
        public async Task<IHttpActionResult> GetCommentsByCommentBankDataGrid([FromUri] int commentBankId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var comments = await _service.GetCommentsByBank(commentBankId);

                var list = comments.Select(_mapping.Map<DataGridCommentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditComments")]
        [Route("comments/update", Name = "ApiUpdateComment")]
        public async Task<IHttpActionResult> UpdateComment([FromBody] CommentDto comment)
        {
            try
            {
                await _service.UpdateComment(comment);
                
                return Ok("Comment updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/profiles")]
    public class ProfilesController : MyPortalApiController
    {
        /// <summary>
        ///     Adds a new log to the database.
        /// </summary>
        /// <param name="log">The log to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("logs/create")]
        public IHttpActionResult CreateLog([FromBody] ProfileLog log)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var userId = User.Identity.GetUserId();

            return PrepareResponse(ProfilesProcesses.CreateLog(log, academicYearId, userId, _context));
        }

        /// <summary>
        ///     Deletes the specified log from the database.
        /// </summary>
        /// <param name="logId">The ID of the log to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("logs/delete/{logId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteLog([FromUri] int logId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteLog(logId, _context));
        }

        /// <summary>
        ///     Gets the specified log from the database.
        /// </summary>
        /// <param name="logId">The ID of the log to fetch.</param>
        /// <returns>Returns a DTO of the specified log.</returns>
        /// <exception cref="HttpResponseException">Thrown when the log is not found.</exception>
        [HttpGet]
        [Route("logs/get/byId/{logId:int}")]
        public ProfileLogDto GetLogById([FromUri] int logId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetLogById(logId, _context));
        }

        /// <summary>
        ///     Get all logs for the specified student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch logs for.</param>
        /// <returns>Returns a list of DTOs of logs for students.</returns>
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

        /// <summary>
        ///     Updates the log in the database.
        /// </summary>
        /// <param name="log">The log to be updated in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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

        /// <summary>
        ///     Adds a comment bank to the database.
        /// </summary>
        /// <param name="commentBank">The comment bank to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("commentBanks/create")]
        public IHttpActionResult CreateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesProcesses.CreateCommentBank(commentBank, _context));
        }

        /// <summary>
        ///     Deletes the specified comment bank.
        /// </summary>
        /// <param name="commentBankId">The ID of the comment bank to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("commentBanks/delete/{commentBankId:int}")]
        public IHttpActionResult DeleteCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteCommentBank(commentBankId, _context));
        }

        /// <summary>
        ///     Gets comment bank with the specified ID.
        /// </summary>
        /// <param name="commentBankId">The ID of the comment bank to get.</param>
        /// <returns>Returns DTO of the specified comment bank.</returns>
        /// <exception cref="HttpResponseException">Thrown when comment bank is not found.</exception>
        [HttpGet]
        [Route("commentBanks/get/byId/{commentBankId:int}")]
        public ProfileCommentBankDto GetCommentBankById([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentBankById(commentBankId, _context));
        }

        /// <summary>
        ///     Gets all comment banks from the database.
        /// </summary>
        /// <returns>Returns a list of DTOs of all comment banks.</returns>
        [HttpGet]
        [Route("commentBanks/get/all")]
        public IEnumerable<ProfileCommentBankDto> GetAllCommentBanks()
        {
            return PrepareResponseObject(ProfilesProcesses.GetAllCommentBanks(_context));
        }

        /// <summary>
        ///     Updates a comment bank in the database.
        /// </summary>
        /// <param name="commentBank">The comment bank to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("commentBanks/update")]
        public IHttpActionResult UpdateCommentBank([FromBody] ProfileCommentBank commentBank)
        {
            return PrepareResponse(ProfilesProcesses.UpdateCommentBank(commentBank, _context));
        }

        /// <summary>
        ///     Adds comment to database.
        /// </summary>
        /// <param name="comment">The comment to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("comments/create")]
        public IHttpActionResult CreateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesProcesses.CreateComment(comment, _context));
        }

        /// <summary>
        ///     Deletes the specified comment from the database.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("comments/delete/{commentId:int}")]
        public IHttpActionResult DeleteComment(int commentId)
        {
            return PrepareResponse(ProfilesProcesses.DeleteComment(commentId, _context));
        }

        /// <summary>
        ///     Gets comment from the database with the specified ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment to fetch.</param>
        /// <returns>Returns a DTO of the specified comment.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("comments/get/byId/{commentId:int}")]
        public ProfileCommentDto GetCommentById([FromUri] int commentId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentById(commentId, _context));
        }

        /// <summary>
        ///     Gets all comments from the database.
        /// </summary>
        /// <returns>Returns a list of all DTOs of all comments.</returns>
        [HttpGet]
        [Route("comments/get/all")]
        public IEnumerable<ProfileCommentDto> GetComments()
        {
            return PrepareResponseObject(ProfilesProcesses.GetAllComments(_context));
        }

        /// <summary>
        ///     Gets all the comments from the specified comment bank.
        /// </summary>
        /// <param name="commentBankId">The ID of the comment bank to get comments from.</param>
        /// <returns>Returns a list of DTOs of comments from the comment bank.</returns>
        [HttpGet]
        [Route("comments/get/byBank/{commentBankId:int}")]
        public IEnumerable<ProfileCommentDto> GetCommentsByCommentBank([FromUri] int commentBankId)
        {
            return PrepareResponseObject(ProfilesProcesses.GetCommentsByBank(commentBankId, _context));
        }

        /// <summary>
        ///     Updates the specified comment bank.
        /// </summary>
        /// <param name="comment">The comment bank to update in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("comments/update")]
        public IHttpActionResult UpdateComment([FromBody] ProfileComment comment)
        {
            return PrepareResponse(ProfilesProcesses.UpdateComment(comment, _context));
        }
    }
}
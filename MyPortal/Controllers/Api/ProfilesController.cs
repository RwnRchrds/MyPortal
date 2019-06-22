using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class ProfilesController : MyPortalApiController
    {
        #region Logs

        /// <summary>
        ///     Adds a new log to the database.
        /// </summary>
        /// <param name="log">The log to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/logs/new")]
        public IHttpActionResult CreateLog(ProfileLog log)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var authorId = log.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                var userId = User.Identity.GetUserId();
                author = PeopleProcesses.GetStaffFromUserId(userId, _context);
                if (author == null) return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
            }

            if (authorId != 0) author = _context.StaffMembers.SingleOrDefault(x => x.Id == authorId);

            if (author == null) return Content(HttpStatusCode.NotFound, "Staff member not found");

            log.Date = DateTime.Now;
            log.AuthorId = author.Id;
            log.AcademicYearId = academicYearId;

            if (!ModelState.IsValid) return Content(HttpStatusCode.BadRequest, "Invalid data");

            _context.ProfileLogs.Add(log);
            _context.SaveChanges();

            return Ok("Log created");
        }

        /// <summary>
        ///     Deletes the specified log from the database.
        /// </summary>
        /// <param name="id">The ID of the log to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/logs/log/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteLog(int id)
        {
            var logInDb = _context.ProfileLogs.SingleOrDefault(l => l.Id == id);

            if (logInDb == null) return Content(HttpStatusCode.NotFound, "Log does not exist");

            logInDb.Deleted = true; //Flag log as deleted instead of removing from database.
            //_context.ProfileLogs.Remove(logInDb);
            _context.SaveChanges();

            return Ok("Log deleted");
        }

        /// <summary>
        ///     Gets the specified log from the database.
        /// </summary>
        /// <param name="id">The ID of the log to fetch.</param>
        /// <returns>Returns a DTO of the specified log.</returns>
        /// <exception cref="HttpResponseException">Thrown when the log is not found.</exception>
        [HttpGet]
        [Route("api/logs/log/{id}")]
        public ProfileLogDto GetLog(int id)
        {
            var log = _context.ProfileLogs.SingleOrDefault(l => l.Id == id);

            if (log == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<ProfileLog, ProfileLogDto>(log);
        }

        /// <summary>
        ///     Get all logs for the specified student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch logs for.</param>
        /// <returns>Returns a list of DTOs of logs for students.</returns>
        [HttpGet]
        [Route("api/logs/{studentId}")]
        public IEnumerable<ProfileLogDto> GetLogs(int studentId)
        {
            if (User.IsInRole("Student")) new StudentsController().AuthenticateStudentRequest(studentId);

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.ProfileLogs.Where(l => l.StudentId == studentId && l.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<ProfileLog, ProfileLogDto>);
        }

        [HttpPost]
        [Route("api/profiles/logs/dataGrid/get/{studentId}")]
        public IHttpActionResult GetLogsForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var logs = _context.ProfileLogs
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted)
                .OrderByDescending(x => x.Date).ToList().Select(Mapper.Map<ProfileLog, GridLogDto>);

            var result = logs.PerformDataOperations(dm);

            if (!dm.RequiresCounts) return Json(result);

            return Json(new {result = result.Items, count = result.Count});
        }

        /// <summary>
        ///     Updates the log in the database.
        /// </summary>
        /// <param name="log">The log to be updated in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/logs/log/edit")]
        [HttpPost]
        public IHttpActionResult UpdateLog(ProfileLog log)
        {
            if (!ModelState.IsValid) return Content(HttpStatusCode.BadRequest, "Invalid data");

            var logInDb = _context.ProfileLogs.SingleOrDefault(l => l.Id == log.Id);

            if (logInDb == null) return Content(HttpStatusCode.NotFound, "Log not found");

            //var c = Mapper.Map(logDto, logInDb);

            logInDb.TypeId = log.TypeId;
            logInDb.Message = log.Message;

            _context.SaveChanges();

            return Ok("Log updated");
        }

        #endregion

        #region Comment Banks

        /// <summary>
        ///     Checks if the specified comment bank has any child comments.
        /// </summary>
        /// <param name="id">The ID of the comment bank to check.</param>
        /// <returns>Returns boolean value.</returns>
        /// <exception cref="HttpResponseException">Thrown when comment bank is not found.</exception>
        [HttpGet]
        [System.Web.Mvc.Route("api/commentBanks/hasComments/{id}")]
        public bool CommentBankHasComments(int id)
        {
            var commentBank = _context.ProfileCommentBanks.SingleOrDefault(x => x.Id == id);

            if (commentBank == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return commentBank.ProfileComments.Any();
        }

        /// <summary>
        ///     Adds a comment bank to the database.
        /// </summary>
        /// <param name="commentBank">The comment bank to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/commentBanks/create")]
        public IHttpActionResult CreateCommentBank(ProfileCommentBank commentBank)
        {
            if (!ModelState.IsValid || commentBank.Name.IsNullOrWhiteSpace())
                return Content(HttpStatusCode.BadRequest, "Invalid data");

            if (_context.ProfileCommentBanks.Any(x => x.Name == commentBank.Name))
                return Content(HttpStatusCode.BadRequest, "Comment bank already exists");

            _context.ProfileCommentBanks.Add(commentBank);
            _context.SaveChanges();
            return Ok("Comment bank added");
        }

        /// <summary>
        ///     Deletes the specified comment bank.
        /// </summary>
        /// <param name="id">The ID of the comment bank to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/commentBanks/delete/{id}")]
        public IHttpActionResult DeleteCommentBank(int id)
        {
            var commentBank = _context.ProfileCommentBanks.SingleOrDefault(x => x.Id == id);

            if (commentBank == null) return Content(HttpStatusCode.NotFound, "Comment bank not found");

            var comments = _context.ProfileComments.Where(x => x.CommentBankId == id);

            if (comments.Any()) _context.ProfileComments.RemoveRange(comments);

            _context.ProfileCommentBanks.Remove(commentBank);
            _context.SaveChanges();
            return Ok("Comment bank deleted");
        }

        /// <summary>
        ///     Gets comment bank with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the comment bank to get.</param>
        /// <returns>Returns DTO of the specified comment bank.</returns>
        /// <exception cref="HttpResponseException">Thrown when comment bank is not found.</exception>
        [HttpGet]
        [Route("api/commentBanks/byId/{id}")]
        public ProfileCommentBankDto GetCommentBankById(int id)
        {
            var commentBankInDb = _context.ProfileCommentBanks.SingleOrDefault(x => x.Id == id);

            if (commentBankInDb == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<ProfileCommentBank, ProfileCommentBankDto>(commentBankInDb);
        }

        /// <summary>
        ///     Gets all comment banks from the database.
        /// </summary>
        /// <returns>Returns a list of DTOs of all comment banks.</returns>
        [HttpGet]
        [Route("api/commentBanks/all")]
        public IEnumerable<ProfileCommentBankDto> GetCommentBanks()
        {
            return _context.ProfileCommentBanks.OrderBy(x => x.Name).ToList()
                .Select(Mapper.Map<ProfileCommentBank, ProfileCommentBankDto>);
        }

        /// <summary>
        ///     Updates a comment bank in the database.
        /// </summary>
        /// <param name="commentBank">The comment bank to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/commentBanks/update")]
        public IHttpActionResult UpdateCommentBank(ProfileCommentBank commentBank)
        {
            var commentBankInDb = _context.ProfileCommentBanks.SingleOrDefault(x => x.Id == commentBank.Id);

            if (commentBankInDb == null) return Content(HttpStatusCode.NotFound, "Comment bank not found");

            commentBankInDb.Name = commentBank.Name;

            _context.SaveChanges();
            return Ok("Comment bank updated");
        }

        #endregion

        #region Comments

        /// <summary>
        ///     Adds comment to database.
        /// </summary>
        /// <param name="comment">The comment to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/comments/create")]
        public IHttpActionResult CreateComment(ProfileComment comment)
        {
            if (!ModelState.IsValid) return Content(HttpStatusCode.BadRequest, "Invalid data");

            _context.ProfileComments.Add(comment);
            _context.SaveChanges();
            return Ok("Comment added");
        }

        /// <summary>
        ///     Deletes the specified comment from the database.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/comments/delete/{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            var comment = _context.ProfileComments.SingleOrDefault(x => x.Id == id);

            if (comment == null) return Content(HttpStatusCode.NotFound, "Comment not found");

            _context.ProfileComments.Remove(comment);
            _context.SaveChanges();

            return Ok("Comment deleted");
        }

        /// <summary>
        ///     Gets comment from the database with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the comment to fetch.</param>
        /// <returns>Returns a DTO of the specified comment.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/comments/byId/{id}")]
        public ProfileCommentDto GetCommentById(int id)
        {
            var comment = _context.ProfileComments.SingleOrDefault(x => x.Id == id);

            if (comment == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<ProfileComment, ProfileCommentDto>(comment);
        }

        /// <summary>
        ///     Gets all comments from the database.
        /// </summary>
        /// <returns>Returns a list of all DTOs of all comments.</returns>
        [HttpGet]
        [Route("api/comments/all")]
        public IEnumerable<ProfileCommentDto> GetComments()
        {
            return _context.ProfileComments
                .OrderBy(x => x.Value)
                .ToList()
                .Select(Mapper.Map<ProfileComment, ProfileCommentDto>);
        }

        /// <summary>
        ///     Gets all the comments from the specified comment bank.
        /// </summary>
        /// <param name="id">The ID of the comment bank to get comments from.</param>
        /// <returns>Returns a list of DTOs of comments from the comment bank.</returns>
        [HttpGet]
        [Route("api/comments/byBank/{id}")]
        public IEnumerable<ProfileCommentDto> GetCommentsByCommentBank(int id)
        {
            return _context.ProfileComments
                .Where(x => x.CommentBankId == id)
                .OrderBy(x => x.Value)
                .ToList()
                .Select(Mapper.Map<ProfileComment, ProfileCommentDto>);
        }

        /// <summary>
        ///     Updates the specified comment bank.
        /// </summary>
        /// <param name="comment">The comment bank to update in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/comments/update")]
        public IHttpActionResult UpdateComment(ProfileComment comment)
        {
            if (!ModelState.IsValid) return Content(HttpStatusCode.BadRequest, "Invalid data");

            var commentInDb = _context.ProfileComments.SingleOrDefault(x => x.Id == comment.Id);

            if (commentInDb == null) return Content(HttpStatusCode.NotFound, "Comment not found");

            commentInDb.Value = comment.Value;
            commentInDb.CommentBankId = comment.CommentBankId;

            _context.SaveChanges();

            return Ok("Comment updated");
        }

        #endregion
    }
}
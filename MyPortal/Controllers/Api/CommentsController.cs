using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    public class CommentsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public CommentsController()
        {
            _context = new MyPortalDbContext();
        }

        public CommentsController(MyPortalDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Adds comment to database.
        /// </summary>
        /// <param name="comment">The comment to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/comments/create")]
        public IHttpActionResult CreateComment(ProfileComment comment)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.ProfileComments.Add(comment);
            _context.SaveChanges();
            return Ok("Comment added");
        }

        /// <summary>
        /// Deletes the specified comment from the database.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/comments/delete/{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            var comment = _context.ProfileComments.SingleOrDefault(x => x.Id == id);

            if (comment == null)
            {
                return Content(HttpStatusCode.NotFound, "Comment not found");
            }

            _context.ProfileComments.Remove(comment);
            _context.SaveChanges();

            return Ok("Comment deleted");
        }

        /// <summary>
        /// Gets comment from the database with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the comment to fetch.</param>
        /// <returns>Returns a DTO of the specified comment.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/comments/byId/{id}")]
        public ProfileCommentDto GetCommentById(int id)
        {
            var comment = _context.ProfileComments.SingleOrDefault(x => x.Id == id);

            if (comment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<ProfileComment, ProfileCommentDto>(comment);
        }

        /// <summary>
        /// Gets all comments from the database.
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
        /// Gets all the comments from the specified comment bank.
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
        /// Updates the specified comment bank.
        /// </summary>
        /// <param name="comment">The comment bank to update in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/comments/update")]
        public IHttpActionResult UpdateComment(ProfileComment comment)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var commentInDb = _context.ProfileComments.SingleOrDefault(x => x.Id == comment.Id);

            if (commentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Comment not found");
            }

            commentInDb.Value = comment.Value;
            commentInDb.CommentBankId = comment.CommentBankId;

            _context.SaveChanges();

            return Ok("Comment updated");
        }
    }
}
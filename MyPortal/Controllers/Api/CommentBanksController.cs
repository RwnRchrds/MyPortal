using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class CommentBanksController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public CommentBanksController()
        {
            _context = new MyPortalDbContext();
        }

        public CommentBanksController(MyPortalDbContext context)
        {
            _context = context;
        }
        
        
/// <summary>
/// Checks if the specified comment bank has any child comments.
/// </summary>
/// <param name="id">The ID of the comment bank to check.</param>
/// <returns>Returns boolean value.</returns>
/// <exception cref="HttpResponseException">Thrown when comment bank is not found.</exception>
        [HttpGet]
        [System.Web.Mvc.Route("api/commentBanks/hasComments/{id}")]
        public bool CommentBankHasComments(int id)
        {
            var commentBank = _context.CommentBanks.SingleOrDefault(x => x.Id == id);

            if (commentBank == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return commentBank.Comments.Any();
        }

/// <summary>
/// Adds a comment bank to the database.
/// </summary>
/// <param name="commentBank">The comment bank to add to the database.</param>
/// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/commentBanks/create")]
        public IHttpActionResult CreateCommentBank(CommentBank commentBank)
        {
            if (!ModelState.IsValid || commentBank.Name.IsNullOrWhiteSpace())
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            if (_context.CommentBanks.Any(x => x.Name == commentBank.Name))
            {
                return Content(HttpStatusCode.BadRequest, "Comment bank already exists");
            }

            _context.CommentBanks.Add(commentBank);
            _context.SaveChanges();
            return Ok("Comment bank added");
        }

/// <summary>
/// Deletes the specified comment bank.
/// </summary>
/// <param name="id">The ID of the comment bank to delete.</param>
/// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/commentBanks/delete/{id}")]
        public IHttpActionResult DeleteCommentBank(int id)
        {
            var commentBank = _context.CommentBanks.SingleOrDefault(x => x.Id == id);

            if (commentBank == null)
            {
                return Content(HttpStatusCode.NotFound, "Comment bank not found");
            }

            var comments = _context.Comments.Where(x => x.CommentBankId == id);

            if (comments.Any())
            {
                _context.Comments.RemoveRange(comments);
            }

            _context.CommentBanks.Remove(commentBank);
            _context.SaveChanges();
            return Ok("Comment bank deleted");
        }

/// <summary>
/// Gets comment bank with the specified ID.
/// </summary>
/// <param name="id">The ID of the comment bank to get.</param>
/// <returns>Returns DTO of the specified comment bank.</returns>
/// <exception cref="HttpResponseException">Thrown when comment bank is not found.</exception>
        [HttpGet]
        [Route("api/commentBanks/byId/{id}")]
        public CommentBankDto GetCommentBankById(int id)
        {
            var commentBankInDb = _context.CommentBanks.SingleOrDefault(x => x.Id == id);

            if (commentBankInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CommentBank, CommentBankDto>(commentBankInDb);
        }

/// <summary>
/// Gets all comment banks from the database.
/// </summary>
/// <returns>Returns a list of DTOs of all comment banks.</returns>
        [HttpGet]
        [Route("api/commentBanks/all")]
        public IEnumerable<CommentBankDto> GetCommentBanks()
        {
            return _context.CommentBanks.OrderBy(x => x.Name).ToList().Select(Mapper.Map<CommentBank, CommentBankDto>);
        }

/// <summary>
/// Updates a comment bank in the database.
/// </summary>
/// <param name="commentBank">The comment bank to update.</param>
/// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/commentBanks/update")]
        public IHttpActionResult UpdateCommentBank(CommentBank commentBank)
        {
            var commentBankInDb = _context.CommentBanks.SingleOrDefault(x => x.Id == commentBank.Id);

            if (commentBankInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Comment bank not found");
            }

            commentBankInDb.Name = commentBank.Name;

            _context.SaveChanges();
            return Ok("Comment bank updated");
        }
    }
}
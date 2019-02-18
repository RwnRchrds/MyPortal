using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/commentBanks/all")]
        public IEnumerable<CommentBankDto> GetCommentBanks()
        {
            return _context.CommentBanks.OrderBy(x => x.Name).ToList().Select(Mapper.Map<CommentBank, CommentBankDto>);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/commentBanks/byId/{id}")]
        public CommentBankDto GetCommentBank(int id)
        {
            var commentBankInDb = _context.CommentBanks.SingleOrDefault(x => x.Id == id);

            if (commentBankInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CommentBank, CommentBankDto>(commentBankInDb);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/commentBanks/create")]
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

            _context.CommentBanks.Add((commentBank));
            _context.SaveChanges();
            return Ok("Comment bank added");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/commentBanks/update")]
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

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/commentBanks/delete/{id}")]
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

        [System.Web.Http.HttpGet]
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
    }
}
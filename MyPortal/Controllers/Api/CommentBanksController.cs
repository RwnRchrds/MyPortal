using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using AutoMapper;
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
            return _context.CommentBanks.ToList().Select(Mapper.Map<CommentBank, CommentBankDto>);
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
        public IHttpActionResult CreateCommentBank(CommentBankDto commentBank)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.CommentBanks.Add(Mapper.Map<CommentBankDto, CommentBank>(commentBank));
            _context.SaveChanges();
            return Ok("Comment bank added");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/commentBanks/update")]
        public IHttpActionResult UpdateCommentBank(CommentBankDto commentBank)
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

            _context.CommentBanks.Remove(commentBank);
            _context.SaveChanges();
            return Ok("Comment bank deleted");
        }
    }
}
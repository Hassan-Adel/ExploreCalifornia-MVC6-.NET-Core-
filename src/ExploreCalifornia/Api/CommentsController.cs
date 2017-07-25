using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCalifornia.Api
{
    
    [Route("api/posts/{postKey}/comments")]
    public class CommentsController : Controller
    {
        private readonly BlogDataContext _db;

        public CommentsController(BlogDataContext db)
        {
            _db = db;
        }

        // GET: api/values
        [HttpGet]
        public IQueryable<Comment> Get(string postKey)
        {
            return _db.Comments.Where(x => x.Post.Key == postKey);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            var comment = _db.Comments.FirstOrDefault(x => x.Id == id);
            return comment;
        }

        //Visual Studio generated also prefixed the comment parameter with the FromBody attribute. 
        //This attribute tells model binding to only consider the body of the Http request when it's attempting to populate the properties of the parameter.
        // POST api/values
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public Comment Post(string postKey, [FromBody]Comment comment)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Key == postKey);

            if (post == null)
                return null;

            comment.Post = post;
            comment.Posted = DateTime.Now;
            comment.Author = User.Identity.Name;

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return comment;
        }

        // PUT api/values/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Comment updatedComment)
        {
            var comment = _db.Comments.FirstOrDefault(x => x.Id == id);

            if (comment == null)
                return NotFound();

            comment.Body = updatedComment.Body;

            _db.SaveChanges();

            return Ok();
        }

        // DELETE api/values/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var comment = _db.Comments.FirstOrDefault(x => x.Id == id);
            if(comment != null)
            {
                _db.Comments.Remove(comment);
                _db.SaveChanges();
            }
        }
    }
}

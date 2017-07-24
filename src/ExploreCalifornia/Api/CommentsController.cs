using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExploreCalifornia.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCalifornia.Api
{
    [Route("api/post/{postKey}/comments")]
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
            return _db.Comments.Where(comment => comment.Post.Key == postKey);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //Visual Studio generated also prefixed the comment parameter with the FromBody attribute. 
        //This attribute tells model binding to only consider the body of the Http request when it's attempting to populate the properties of the parameter.
        // POST api/values
        [HttpPost]
        public Comment Post(string postKey, [FromBody]Comment comment)
        {
            var Post = _db.Posts.Where(post => post.Key == postKey).FirstOrDefault();
            if (Post == null)
                return null;

            comment.Post = Post;
            comment.Posted = DateTime.Now;
            _db.Add(comment);
            _db.SaveChanges();
            return comment;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

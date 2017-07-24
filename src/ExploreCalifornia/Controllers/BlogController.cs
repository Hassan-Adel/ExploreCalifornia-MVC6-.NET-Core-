using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExploreCalifornia.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        [Route("")]
        public IActionResult Index()
        {
            var posts = new[]
            {
                new Post
            {
                Title = "My Post 1",
                Posted = DateTime.Now,
                Author = "Hassan Adel",
                Body = "My First post on this site !!"
            },
                new Post
            {
                Title = "My Post 2",
                Posted = DateTime.Now,
                Author = "Hassan Adel",
                Body = "My First post on this site !!"
            },
                new Post
            {
                Title = "My Post 3",
                Posted = DateTime.Now,
                Author = "Hassan Adel",
                Body = "My First post on this site !!"
            }
            };
            return View(posts);
        }

        //to customize the URL for this controller action, we'll simply place the route attribute on
        //top of the action and pass it a string parameter that defines the custom route pattern
        //to apply to just this action.
        //------Constrains------
        //When the URL seems like it matches the pattern but doesn't meet these constraints, 
        //MVC does not map it to this controller action and we end up with an empty page
        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = new Post
            {
                Title = "My Post",
                Posted = DateTime.Now,
                Author = "Hassan Adel",
                Body = "My First post on this site !!"
            };
            return View(post);
        }

        // GET: /<blog>/create
        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // Post: /<blog>/create
        [HttpPost, Route("create")]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return View(); 

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            _db.Posts.Add(post);
            _db.SaveChanges();

            return View();
        }

    }
}

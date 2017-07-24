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
        public IActionResult Index(int page=0)
        {
            var pageSize = 2;
            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts =
                _db.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();
            /*
             *  Jquery adds a special header named X - Requested - With on every single one of its AJAX requests. 
             *  So all we'll need to do is check to see if that header is there, and then we'll know that it's an AJAX request.
             *  Then when it is an AJAX request instead of calling the view method, like we're doing now, to return the full view including the layout, 
             *  we'll instead call the partial view method which will skip the layout and render only the view as a partial view.
             */

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return PartialView(posts);

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
            var post = _db.Posts.FirstOrDefault(x => x.Key == key);
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

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }

    }
}

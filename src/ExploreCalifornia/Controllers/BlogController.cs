using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        // GET: /<controller>/
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        //to customize the URL for this controller action, we'll simply place the route attribute on
        //top of the action and pass it a string parameter that defines the custom route pattern
        //to apply to just this action.
        //------Constrains------
        //When the URL seems like it matches the pattern but doesn't meet these constraints, 
        //MVC does not map it to this controller action and we end up with an empty page
        [Route("blog/{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            return new ContentResult
            {
                Content = string.Format("Year: {0};  Month: {1};  Key: {2}",
                                        year, month, key)
            };
        }
    }
}

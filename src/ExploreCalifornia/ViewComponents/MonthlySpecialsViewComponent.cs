using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.ViewComponents
{
    //MVC require at least one of the following things to be in place in order to recognize a class as a view component:
    //The first requirement is that the name of the class ends with the suffix view component.or
    //The second criteria could be that the class is decorated with the view component attribute. or 
    //The third criteria is that the class can extend from the view component base class.
    //Any one of them will work
    [ViewComponent]
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        private readonly SpecialDataContext _specials;
        public MonthlySpecialsViewComponent(SpecialDataContext specials)
        {
            _specials = specials;
        }
        public IViewComponentResult Invoke()
        {

            var SiteSpecials = _specials.GetMonthlySpecials();
            return View(SiteSpecials);
         }
    }
}

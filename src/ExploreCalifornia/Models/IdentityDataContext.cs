using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExploreCalifornia.Models
{
    public class IdentityDataContext : IdentityDbContext<IdentityUser>
    {
        //Add a constructor that accepts a Db context options object and just passes it to the Db context base class like this
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

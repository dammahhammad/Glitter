using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Microsite.Models
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
        }

        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<NewTweetModel> Tweets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Microsite.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsite.Data
{
    public class MicrositeDbContext : DbContext
    { 
        public MicrositeDbContext() 
        {
            
        }

        public MicrositeDbContext(DbContextOptions<MicrositeDbContext> options) : base(options)
        {

        }
        public DbSet<TagDTO> Tag { get; set; }
        public DbSet<UserRegisterDTO> Users { get; set; }
        public DbSet<NewTweetDTO> Tweets { get; set; }
        public DbSet<FollowModelDTO> Follow { get; set; }
        public DbSet<LikeTweetDTO> LikeTweet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                     .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Microsite/appsettings.json")
                                                                     .Build();*/

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                                                                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                                     .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDBConnection"));
        }
    }
}

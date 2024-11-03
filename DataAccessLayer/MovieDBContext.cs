using DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MovieDBContext : DbContext
    {
        public DbSet<user_info> user_info { get; set; }
        public DbSet<title_basics> title_basics { get; set; }
        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user_info>().HasKey(u => u.user_id);
            //modelBuilder.Entity<Movies>().ToTable("title_basics");
            modelBuilder.Entity<title_basics>().HasKey(u => u.tconst);

        }
    }
}

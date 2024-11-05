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
        public DbSet<NameBasics> nameBasics { get; set; }
        public DbSet<title_principals> title_Principals { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user_info>().HasKey(u => u.user_id);
            //modelBuilder.Entity<Movies>().ToTable("title_basics");
            modelBuilder.Entity<title_basics>().HasKey(u => u.tconst);

            modelBuilder.Entity<NameBasics>().ToTable("name_basics");
            modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<NameBasics>().HasKey(n => n.nconst);

            modelBuilder.Entity<title_basics>().HasKey(t => t.tconst);

            modelBuilder.Entity<title_principals>().ToTable("title_principals");
            modelBuilder.Entity<title_principals>().HasKey(e => new { e.tconst, e.nconst });
            modelBuilder.Entity<title_principals>().HasOne(e => e.nameBasics).WithMany(t => t.Title_Principals).HasForeignKey(e => e.nconst);
            modelBuilder.Entity<title_principals>().HasOne(e => e.titleBasics).WithMany(t => t.title_Principals).HasForeignKey(e => e.tconst);

            modelBuilder.Entity<Notes>().ToTable("notes");
            modelBuilder.Entity<Notes>().HasKey(u => u.NoteId);
            modelBuilder.Entity<Notes>().Property(x => x.NoteId).HasColumnName("note_id");
            modelBuilder.Entity<Notes>().Property(x => x.UserId).HasColumnName("user_id");
            modelBuilder.Entity<Notes>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Notes>().Property(x => x.Note).HasColumnName("note");
            modelBuilder.Entity<Notes>().Property(x => x.CreatedDate).HasColumnName("created_at");
            modelBuilder.Entity<Notes>().Property(x => x.UpdatedDate).HasColumnName("updated_at");
            modelBuilder.Entity<Notes>().HasOne(e => e.User).WithMany(t => t.Notes).HasForeignKey(e => e.UserId);
            modelBuilder.Entity<Notes>().HasOne(e => e.Movie).WithMany(t => t.Notes).HasForeignKey(e => e.Tconst);


        }
    }
}

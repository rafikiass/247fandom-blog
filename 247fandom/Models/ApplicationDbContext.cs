using System;
using Microsoft.EntityFrameworkCore;

namespace _247fandom.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Community> Community { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<PostVote> PostVotes { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=");
        }

    }
}

//dotnet aspnet-codegenerator -p 247fandom controller -name VotesController -async -api -m PostVote -dc ApplicationDbContext  -outDir Controllers
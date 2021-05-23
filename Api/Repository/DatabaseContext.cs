using Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User {Id = 1, Name = "Donald Draper"},
                new User {Id = 2, Name = "Trudy Campbell"},
                new User {Id = 3, Name = "Roger Sterling"},
                new User {Id = 4, Name = "Pete Campbell"},
                new User {Id = 5, Name = "Bert Cooper"}
            );
        }
    }
}
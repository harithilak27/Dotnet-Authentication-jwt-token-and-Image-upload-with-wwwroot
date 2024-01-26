using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;
using Nethi.Models;

namespace AngularAuthYtAPI.Context
{
    public class AppDbContext : DbContext
    {
        internal object ImageEntities;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");
        }
    }
}

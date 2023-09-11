using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using SiPerpusApi.Models;

namespace SiPerpusApi.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Env.Load();
            string connectionString = Env.GetString("CONNECTION_STRING");
            
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                // Handle the case where the connection string is not found or invalid.
                throw new InvalidOperationException("Connection string not found or invalid.");
            }
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
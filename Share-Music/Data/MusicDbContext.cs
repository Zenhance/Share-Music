using Microsoft.EntityFrameworkCore;
using Share_Music.Models;

namespace Share_Music.Data
{
    public class MusicDbContext:DbContext
    {
        private readonly string? connString;
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }

        public DbSet <User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(connString))
            {
                optionsBuilder.UseSqlServer(connString);
            }
        }
    }
}

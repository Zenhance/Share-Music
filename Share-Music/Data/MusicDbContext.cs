using Microsoft.EntityFrameworkCore;
using Share_Music.Models;

namespace Share_Music.Data
{
    public class MusicDbContext:DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }

        public DbSet <User> Users { get; set; }
    }
}

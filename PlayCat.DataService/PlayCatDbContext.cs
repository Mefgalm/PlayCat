using Microsoft.EntityFrameworkCore;

namespace PlayCat.DataService
{
    public class PlayCatDbContext : DbContext
    {
        public PlayCatDbContext(DbContextOptions<PlayCatDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

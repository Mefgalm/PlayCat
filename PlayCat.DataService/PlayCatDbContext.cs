using Microsoft.EntityFrameworkCore;
using PlayCat.DataModel;

namespace PlayCat.DataService
{
    public class PlayCatDbContext : DbContext
    {
        public PlayCatDbContext(DbContextOptions<PlayCatDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audio>()
                .HasIndex(x => x.UniqueIdentifier)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Audio> Audios { get; set; }
    }
}

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

            modelBuilder.Entity<AudioPlaylist>()
                .HasKey(x => new { x.PlaylistId, x.AudioId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Audio> Audios { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<AudioPlaylist> AudioPlaylists { get; set; }

        public DbSet<AuthToken> AuthTokens { get; set; }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayCat.DataService;

namespace PlayCat.DataService.Migrations
{
    [DbContext(typeof(PlayCatDbContext))]
    partial class PlayCatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlayCat.DataModel.Audio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessUrl")
                        .IsRequired();

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Extension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("PhysicUrl")
                        .IsRequired();

                    b.Property<string>("Song")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UniqueIdentifier")
                        .IsRequired();

                    b.Property<Guid?>("UploaderId");

                    b.HasKey("Id");

                    b.HasIndex("UniqueIdentifier")
                        .IsUnique();

                    b.HasIndex("UploaderId");

                    b.ToTable("Audios");
                });

            modelBuilder.Entity("PlayCat.DataModel.AudioPlaylist", b =>
                {
                    b.Property<Guid>("PlaylistId");

                    b.Property<Guid>("AudioId");

                    b.Property<DateTime>("DateCreated");

                    b.HasKey("PlaylistId", "AudioId");

                    b.HasIndex("AudioId");

                    b.ToTable("AudioPlaylists");
                });

            modelBuilder.Entity("PlayCat.DataModel.AuthToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateExpired");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("PlayCat.DataModel.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("PlayCat.DataModel.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .HasMaxLength(100);

                    b.Property<bool>("IsUploadingAudio");

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.Property<string>("NickName")
                        .HasMaxLength(100);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSald")
                        .IsRequired();

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("VerificationCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlayCat.DataModel.Audio", b =>
                {
                    b.HasOne("PlayCat.DataModel.User", "Uploader")
                        .WithMany()
                        .HasForeignKey("UploaderId");
                });

            modelBuilder.Entity("PlayCat.DataModel.AudioPlaylist", b =>
                {
                    b.HasOne("PlayCat.DataModel.Audio", "Audio")
                        .WithMany()
                        .HasForeignKey("AudioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayCat.DataModel.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayCat.DataModel.AuthToken", b =>
                {
                    b.HasOne("PlayCat.DataModel.User", "User")
                        .WithOne("AuthToken")
                        .HasForeignKey("PlayCat.DataModel.AuthToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayCat.DataModel.Playlist", b =>
                {
                    b.HasOne("PlayCat.DataModel.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}

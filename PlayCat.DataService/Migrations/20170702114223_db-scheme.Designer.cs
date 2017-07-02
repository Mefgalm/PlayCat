using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayCat.DataService;

namespace PlayCat.DataService.Migrations
{
    [DbContext(typeof(PlayCatDbContext))]
    [Migration("20170702114223_db-scheme")]
    partial class dbscheme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.HasIndex("UniqueIdentifier")
                        .IsUnique();

                    b.ToTable("Audios");
                });

            modelBuilder.Entity("PlayCat.DataModel.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.Property<string>("NickName")
                        .HasMaxLength(100);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSald")
                        .IsRequired();

                    b.Property<DateTime>("RegisterDate");

                    b.Property<Guid>("VerificationCode");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}

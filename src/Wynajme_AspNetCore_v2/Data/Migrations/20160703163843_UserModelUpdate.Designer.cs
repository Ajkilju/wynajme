using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wynajme_AspNetCore_v2.Data;

namespace Wynajme_AspNetCore_v2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160703163843_UserModelUpdate")]
    partial class UserModelUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<byte[]>("Image");

                    b.Property<string>("ImageMimeType");

                    b.Property<DateTime>("LastLogIn");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ImageContent");

                    b.Property<string>("ImageMimeType");

                    b.Property<int>("OgloszenieId");

                    b.HasKey("ImageId");

                    b.HasIndex("OgloszenieId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Kategoria", b =>
                {
                    b.Property<int>("KategoriaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("KategoriaId");

                    b.ToTable("Kategoria");
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Miasto", b =>
                {
                    b.Property<int>("MiastoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("MiastoId");

                    b.ToTable("Miasto");
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Obserwowane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OgloszenieId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OgloszenieId");

                    b.HasIndex("UserId");

                    b.ToTable("Obserwowane");
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Ogloszenie", b =>
                {
                    b.Property<int>("OgloszenieId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cena");

                    b.Property<DateTime>("DataDodania");

                    b.Property<string>("DodatkoweWyposazenie")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("KategoriaId");

                    b.Property<bool>("KuchniaEl");

                    b.Property<bool>("KuchniaGaz");

                    b.Property<bool>("Lodowka");

                    b.Property<int>("MiastoId");

                    b.Property<bool>("Mikrofala");

                    b.Property<byte[]>("Miniature");

                    b.Property<string>("MiniatureMimeType");

                    b.Property<int>("Powierzchnia");

                    b.Property<bool>("Pralka");

                    b.Property<bool>("Prysznic");

                    b.Property<string>("Telefon");

                    b.Property<string>("Tresc")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4000);

                    b.Property<string>("Tytul")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UserId");

                    b.Property<bool>("Wanna");

                    b.Property<bool>("Zmywarka");

                    b.HasKey("OgloszenieId");

                    b.HasIndex("KategoriaId");

                    b.HasIndex("MiastoId");

                    b.HasIndex("UserId");

                    b.ToTable("Ogloszenie");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Wynajme_AspNetCore_v2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Wynajme_AspNetCore_v2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wynajme_AspNetCore_v2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Image", b =>
                {
                    b.HasOne("Wynajme_AspNetCore_v2.Models.Ogloszenie")
                        .WithMany()
                        .HasForeignKey("OgloszenieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Obserwowane", b =>
                {
                    b.HasOne("Wynajme_AspNetCore_v2.Models.Ogloszenie")
                        .WithMany()
                        .HasForeignKey("OgloszenieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wynajme_AspNetCore_v2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Wynajme_AspNetCore_v2.Models.Ogloszenie", b =>
                {
                    b.HasOne("Wynajme_AspNetCore_v2.Models.Kategoria")
                        .WithMany()
                        .HasForeignKey("KategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wynajme_AspNetCore_v2.Models.Miasto")
                        .WithMany()
                        .HasForeignKey("MiastoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wynajme_AspNetCore_v2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}

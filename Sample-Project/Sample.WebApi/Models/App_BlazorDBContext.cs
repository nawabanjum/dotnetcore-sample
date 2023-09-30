using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sample.WebApi.Models
{
    public partial class App_BlazorDBContext : IdentityDbContext<UserPofile>
    {
        public App_BlazorDBContext()
        {
        }

        public App_BlazorDBContext(DbContextOptions<App_BlazorDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-T29Q4VV;Database=App_BlazorDB;User Id=sa;Password=123;Trusted_Connection=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(

                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "bda30051-c030-467c-93e8-ae3e0b5bee4e"
                },
                 new IdentityRole
                 {
                     Name = "User",
                     NormalizedName = "USER",
                     Id = "e15c12c3-5582-4711-9306-984e0df1dcdd"
                 }
           );

            var hash = new PasswordHasher<UserPofile>();
            modelBuilder.Entity<UserPofile>().HasData(

               new UserPofile
               {
                   Id = "2314094f-0974-4783-ae24-97b801faf01d",
                   Email = "admin@admin.com",
                   NormalizedEmail = "ADMIN@ADMIN.COM",
                   PasswordHash = hash.HashPassword(null, "admin@123#Admin"),
                   UserName = "admin@admin.com",
                   NormalizedUserName = "ADMIN@ADMIN.COM",
                   FirstName = "System",
                   Lastname = "Admin",
                   ProfilePicture="noimage.png"
               },
                new UserPofile
                {
                    Id = "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1",
                    Email = "user@user.com",
                    NormalizedEmail = "USER@USER.COM",
                    PasswordHash = hash.HashPassword(null, "user@123#User"),
                    UserName = "user@user.com",
                    NormalizedUserName = "USER@USER.COM",
                    FirstName = "System",
                    Lastname = "User",
                    ProfilePicture = "noimage.png"
                }
          );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(

               new IdentityUserRole<string>
               {
                   UserId = "2314094f-0974-4783-ae24-97b801faf01d",
                   RoleId = "bda30051-c030-467c-93e8-ae3e0b5bee4e"
               },
                new IdentityUserRole<string>
                {
                    UserId = "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1",
                    RoleId = "e15c12c3-5582-4711-9306-984e0df1dcdd"

                }
          );




        }
        public DbSet<Product> Products { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

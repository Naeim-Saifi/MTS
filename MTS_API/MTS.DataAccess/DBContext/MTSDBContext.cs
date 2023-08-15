using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.DataAccess.DBContext
{
    public partial class MTSDBContext : IdentityDbContext<User, Role, int>
    {
        public MTSDBContext()
        {
        }

        public MTSDBContext(DbContextOptions<MTSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Medicine> Medicine { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-D9H1L4Q;Initial Catalog=MTS_V1;Trusted_Connection=True;");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Identity

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users", schema: "dbo");
                entity.Property(e => e.Id).HasColumnName("UserId");

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Role", schema: "dbo");
                entity.Property(e => e.Id).HasColumnName("RoleId");

            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaim", "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.Id).HasColumnName("UserClaimId");

            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogin", "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");

            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaim", "dbo");
                entity.Property(e => e.Id).HasColumnName("RoleClaimId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRole", "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");

            });


            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserToken", "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");

            });

            #endregion

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.Property(e => e.Brand).HasMaxLength(200);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.FullName).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

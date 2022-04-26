using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Back_End.Models;

namespace User_Back_End.DAL
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(User => User.ID).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Role>().Property(Role => Role.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserID, ur.RoleID });
            modelBuilder.Entity<User>(b =>
            {
                b.HasMany(e => e.Roles)
                .WithOne(e => e.user)
                .HasForeignKey(ur => ur.UserID)
                .IsRequired();
            });
            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.role)
                .HasForeignKey(ur => ur.RoleID)
                .IsRequired();
            });
            //modelBuilder.Entity<UserRole>().HasOne(ur => ur.user).WithMany(u => u.Roles).HasForeignKey(ur => ur.UserID);
            //modelBuilder.Entity<UserRole>().HasOne(ur => ur.role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleID);
        }
    }
}

using ExpressMicroPermissions.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressMicroPermissions.Data
{
    public class PermissionContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=DESKTOP-QJ02OLT\SQLEXPRESS;Database=Security;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPermission>().HasKey(c => new { c.UserId, c.PermissionId });
        }
    }
}

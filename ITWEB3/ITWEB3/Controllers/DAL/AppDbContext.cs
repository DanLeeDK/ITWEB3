using ITWEB3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITWEB3.Controllers.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ComponentTypeCategory>()
                .HasKey(bc => new { bc.CategoryId, bc.ComponentTypeId });

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(bc => bc.ComponentType)
                .WithMany(b => b.ComponentTypeCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.ComponentTypeCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ESImage> ESImages { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEBAfl3.Models;

namespace WEBAfl3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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
        public DbSet<ComponentTypeCategory> CategoryComponentTypes { get; set; }

    }
}

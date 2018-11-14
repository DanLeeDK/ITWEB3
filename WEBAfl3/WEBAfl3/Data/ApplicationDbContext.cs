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

        public ApplicationDbContext(string connectionString) : base(GetOptions(connectionString))
        { }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseSqlServer(connectionString)
                .Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to Many
            modelBuilder.Entity<Component>()
                .HasOne(x => x.ComponentType)
                .WithMany(x => x.Components)
                .OnDelete(DeleteBehavior.SetNull);

            // Many to Many
            modelBuilder.Entity<ComponentTypeCategory>()
                .HasKey(bc => new { bc.CategoryId, bc.ComponentTypeId });

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(bc => bc.ComponentType)
                .WithMany(b => b.ComponentTypeCategories)
                .HasForeignKey(bc => bc.ComponentTypeId);

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.ComponentTypeCategories)
                .HasForeignKey(bc => bc.CategoryId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ESImage> ESImages { get; set; }
        public DbSet<ComponentTypeCategory> CategoryComponentTypes { get; set; }

    }
}

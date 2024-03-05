using Microsoft.EntityFrameworkCore;
using reviewsapp.models;

namespace reviewsapp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<OwnerName> OwnerName { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelOwner> ModelOwners { get; set; }
        public DbSet<ModelCategory> ModelCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelCategory>()
                .HasKey(pc => new { pc.modelId, pc.categoryId });

            modelBuilder.Entity<ModelCategory>()
                .HasOne(pc => pc.Model)
                .WithMany(m => m.ModelCategories)
                .HasForeignKey(pc => pc.modelId);

            modelBuilder.Entity<ModelCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ModelCategories)
                .HasForeignKey(pc => pc.categoryId); // Assuming categoryId is of type int


            modelBuilder.Entity<ModelOwner>()
                .HasKey(po => new { po.modelId, po.OwnerId });

            modelBuilder.Entity<ModelOwner>()
                .HasOne(po => po.Model)
                .WithMany(m => m.ModelOwners)
                .HasForeignKey(po => po.modelId);

            modelBuilder.Entity<ModelOwner>()
                .HasOne(po => po.Owner)
                .WithMany(o => o.ModelOwners)
                .HasForeignKey(po => po.OwnerId);

        }

    }

}

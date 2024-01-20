using CarMarketAnalysis.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMarketAnalysis.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>(entity => {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.Model)
                    .WithMany(m => m.Cars)
                    .HasForeignKey(c => c.ModelId);
            });

            builder.Entity<Brand>(entity => {
                entity.HasKey(b => b.Id);

                entity.HasMany(c => c.Models)
                    .WithOne(m => m.Brand)
                    .HasForeignKey(m => m.BrandId);
            });

            builder.Entity<Model>(entity => {
                entity.HasKey(m => m.Id);

                entity.HasOne(m => m.Brand)
                    .WithMany(b => b.Models)
                    .HasForeignKey(m => m.BrandId);

                entity.HasMany(m => m.Cars)
                    .WithOne(c => c.Model)
                    .HasForeignKey(c => c.ModelId);
            });

            base.OnModelCreating(builder);
        }
    }
}

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
        public DbSet<Generation> Generations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>(entity => {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.Generation)
                    .WithMany(g => g.Cars)
                    .HasForeignKey(g => g.GenerationId);
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

                entity.HasMany(m => m.Generations)
                    .WithOne(g => g.Model)
                    .HasForeignKey(g => g.ModelId);
            });

            builder.Entity<Generation>(entity => {
                entity.HasKey(g => g.Id);

                entity.HasOne(g => g.Model)
                    .WithMany(m => m.Generations)
                    .HasForeignKey(g => g.ModelId);

                entity.HasMany(g => g.Cars)
                    .WithOne(c => c.Generation)
                    .HasForeignKey(c => c.GenerationId);
            });

            base.OnModelCreating(builder);
        }
    }
}

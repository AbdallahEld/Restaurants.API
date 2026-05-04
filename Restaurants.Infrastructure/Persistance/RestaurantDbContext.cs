using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    internal class RestaurantDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DG8T01C\\SQLEXPRESS;DataBase=Restaurants;Trusted_Connection=true;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                        .OwnsOne(r => r.Address);

            modelBuilder.Entity<Dish>()
                        .Property(r => r.Price)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Restaurant>()
                        .HasMany(r => r.Dishes)
                        .WithOne()
                        .HasForeignKey(r => r.RestaurantId);

            base.OnModelCreating(modelBuilder);
        }

        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }
    }
}

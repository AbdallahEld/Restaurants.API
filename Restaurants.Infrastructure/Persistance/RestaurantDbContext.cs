using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : IdentityDbContext<User>(options)
    {
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

            modelBuilder.Entity<User>()
                        .HasMany(u => u.OwnedRestaurants)
                        .WithOne(r => r.Owner)
                        .HasForeignKey(r => r.OwnerId);

            base.OnModelCreating(modelBuilder);
        }

        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }
    }
}

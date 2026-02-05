using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CarRentalApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<CarInWishlist> CarInWishlists { get; set; }
        public virtual DbSet<CarInRent> CarsInRents { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
    }
}

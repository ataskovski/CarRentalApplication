using CarRentalApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Domain.DomainModels
{
    public class Wishlist : BaseEntity
    {
        public string? OwnerId { get; set; }
        public CarRentalApplicationUser? Owner { get; set; }

        public virtual ICollection<CarInWishlist>? CarsInWishlists { get; set; }
    }
}

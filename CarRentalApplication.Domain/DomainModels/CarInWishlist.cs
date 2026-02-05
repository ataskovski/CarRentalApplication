using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Domain.DomainModels
{
    public class CarInWishlist : BaseEntity
    {
        public Guid CarId { get; set; }
        public Car? Car { get; set; }

        public Guid WishlistId { get; set; }
        public Wishlist? Wishlist { get; set; }

        public int Days { get; set; }

        public DateTime AddedAt { get; set; }
    }
}

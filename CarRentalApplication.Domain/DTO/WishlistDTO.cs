using CarRentalApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Domain.DTO
{
    public class WishlistDTO
    {
        public List<CarInWishlist> Cars { get; set; }
        public double TotalPrice { get; set; }
    }
}

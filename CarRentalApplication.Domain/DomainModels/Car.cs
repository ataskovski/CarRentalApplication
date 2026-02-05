using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Domain.DomainModels
{
    public class Car : BaseEntity
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Category { get; set; }

        public double PricePerDay { get; set; }
        public bool IsAvailable { get; set; }

        public virtual ICollection<CarInWishlist>? CarInWishlists { get; set; }
        public virtual ICollection<CarInRent>? CarsInRent { get; set; }
    }
}

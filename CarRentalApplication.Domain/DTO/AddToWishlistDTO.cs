using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Domain.DTO
{
    public class AddToWishlistDTO
    {
        public Guid CarId { get; set; }
        public string? Make { get; set; }
        public string? CarModel { get; set; }
        public int Days { get; set; }
    }
}

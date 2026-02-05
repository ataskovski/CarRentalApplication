using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;


namespace CarRentalApplication.Domain.Identity
{
    public class CarRentalApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public Wishlist? Wishlist { get; set; }
        
    }
}

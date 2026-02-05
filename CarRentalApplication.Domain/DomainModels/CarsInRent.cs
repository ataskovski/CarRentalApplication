using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Domain.DomainModels
{
    public class CarInRent : BaseEntity
    {
        public Guid RentId { get; set; }
        public Rent? Rent { get; set; }
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
        public int Days { get; set; }
    }
}

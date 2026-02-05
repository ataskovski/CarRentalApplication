using CarRentalApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Domain.DomainModels
{
    public class Rent : BaseEntity
    {
        public string? OwnerId { get; set; }
        public CarRentalApplicationUser Owner {  get; set; }
        public virtual ICollection<CarInRent>? CarsInRent {  get; set; }
        public bool IsFinished { get; set; }
    }
}

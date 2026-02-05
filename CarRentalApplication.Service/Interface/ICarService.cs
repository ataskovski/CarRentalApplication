using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Service.Interface
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car? GetById(Guid Id);
        Car Update(Car car);
        Car DeleteById(Guid Id);
        Car Add(Car car);
        void AddToWishlist(AddToWishlistDTO model, Guid userId);
    }
}

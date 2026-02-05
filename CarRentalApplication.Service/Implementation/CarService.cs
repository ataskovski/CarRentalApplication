using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Domain.DTO;
using CarRentalApplication.Repository;
using CarRentalApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Service.Implementation
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IWishlistService _wishlistService;
        private readonly IRepository<CarInWishlist> _carInWishlistRepo;

        public CarService(IRepository<Car> carRepository, IWishlistService wishlistService, IRepository<CarInWishlist> carInWishlistRepo)
        {
            _carRepository = carRepository;
            _wishlistService = wishlistService;
            _carInWishlistRepo = carInWishlistRepo;
        }

        public Car Add(Car car)
        {
            car.Id = Guid.NewGuid();
            return _carRepository.Insert(car);
        }

        public void AddToWishlist(AddToWishlistDTO model, Guid userId)
        {
            var wishlist = _wishlistService.GetByUserId(userId);

            var car = _carRepository.Get(
                selector: x => x,
                predicate: x => x.Id == model.CarId
            );
            

            if (car == null)
                throw new Exception($"{model.CarId} Car does not exist in database");

            var existing = _carInWishlistRepo.Get(
                selector: x => x,
                predicate: x => x.CarId == model.CarId && x.WishlistId == wishlist.Id
            );

            if (existing == null)
            {
                var newCar = new CarInWishlist
                {
                    Id = Guid.NewGuid(),
                    CarId = model.CarId,
                    WishlistId = wishlist.Id,
                    AddedAt = DateTime.Now,
                    Days = model.Days
                };

                _carInWishlistRepo.Insert(newCar);
            }
            else
            {
                existing.Days += model.Days;
                _carInWishlistRepo.Update(existing);
            }
        }


        public Car DeleteById(Guid Id)
        {
            var car = _carRepository.Get(selector: x => x, 
                predicate: x => x.Id == Id);
            return _carRepository.Delete(car);

        }

        public List<Car> GetAll()
        {
            return _carRepository.GetAll(selector:x => x).ToList();
        }

        public Car? GetById(Guid Id)
        {
            return _carRepository.Get(selector: x=>x,
                predicate: x => x.Id == Id);
        }

        public Car Update(Car car)
        {
            return _carRepository.Update(car);
        }
    }
}

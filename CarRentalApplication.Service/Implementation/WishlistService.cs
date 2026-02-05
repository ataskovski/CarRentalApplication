using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Domain.DTO;
using CarRentalApplication.Repository;
using CarRentalApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Service.Implementation
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<Wishlist> _wishlistRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<CarInWishlist> _carInWishlistRepository;
        private readonly IRepository<Rent> _rentRepository;
        private readonly IRepository<CarInRent> _carInRentRepository;

        public WishlistService(IRepository<Wishlist> wishlistRepository, IRepository<Car> carRepository, IRepository<CarInWishlist> carInWishlistRepository, IRepository<Rent> rentRepository, IRepository<CarInRent> carInRentRepository)
        {
            _wishlistRepository = wishlistRepository;
            _carRepository = carRepository;
            _carInWishlistRepository = carInWishlistRepository;
            _rentRepository = rentRepository;
            _carInRentRepository = carInRentRepository;
        }

        public bool DeleteFromWishlist(Guid id, string userId)
        {
            var wishlist = _wishlistRepository.Get(selector: x => x,
                                                predicate: x => x.OwnerId == userId);
            var carToDelete = _carInWishlistRepository.Get(selector: x => x,
                predicate: x => x.CarId == id && x.Wishlist == wishlist);

            _carInWishlistRepository.Delete(carToDelete);
            return true;
        }

        public Wishlist GetByUserId(Guid id)
        {
            return _wishlistRepository.Get(selector: x => x,
                predicate: x=>x.OwnerId == id.ToString());
        }

        public WishlistDTO GetByUserIdIncludingCars(Guid id)
        {
            var wishlist = _wishlistRepository.Get(selector: x => x,
                predicate: x => x.OwnerId == id.ToString(),
                include:x=>x.Include(y=>y.CarsInWishlists).ThenInclude(z=>z.Car));

            var allCars = wishlist.CarsInWishlists.ToList();

            var allCarsPrices = allCars.Select(x => new
            {
                CarPrice = x.Car.PricePerDay,
                DaysForRent = x.Days
            });

            double total = 0.0;
            foreach (var item in allCarsPrices)
            {
                total += item.CarPrice * item.DaysForRent;
            }

            WishlistDTO model = new WishlistDTO
            {
                Cars = allCars,
                TotalPrice = total,
            };
            return model;
        }

        public AddToWishlistDTO GetCarInfo(Guid id)
        {
            var car = _carRepository.Get(selector: x => x,
                predicate: x => x.Id == id);
            var dto = new AddToWishlistDTO
            {
                CarId = car.Id,
                CarModel = car.Model,
                Make = car.Make,
                Days = 1
            };
            return dto;
        }

        public bool RentCars(string userId)
        {
            var wishlist = _wishlistRepository.Get(selector: x => x,
                    predicate: x => x.OwnerId == userId,
                    include: x => x.Include(y => y.CarsInWishlists).ThenInclude(z => z.Car));

            var newRent = new Rent
            {
                Id = Guid.NewGuid(),
                Owner = wishlist.Owner,
                OwnerId = userId
            };
            _rentRepository.Insert(newRent);

            var carsInRent = wishlist.CarsInWishlists.Select(z => new CarInRent
            {
                Car = z.Car,
                CarId = z.CarId,
                Rent = newRent,
                RentId = newRent.Id,
                Days = z.Days
            });

            var total = 0.0;

            foreach (var item in carsInRent)
            {
                total += (item.Days * item.Car.PricePerDay);
                _carInRentRepository.Insert(item);
            }

            wishlist.CarsInWishlists.Clear();
            _wishlistRepository.Update(wishlist);
            return true;
        }

        public bool FinishRent(Guid rentId, string userId)
        {
            var rent = _rentRepository.Get(selector: x => x,
                predicate: x => x.Id == rentId && x.OwnerId == userId,
                include: x => x.Include(r => r.CarsInRent).ThenInclude(c => c.Car));

            if (rent == null)
                return false;

            rent.IsFinished = true;
            _rentRepository.Update(rent);
            return true;
        }
    }
}

using CarRentalMockAPI.Models;

namespace CarRentalMockAPI.Services;

public interface ICarService
{
    List<Car> GetAllCars();
    Car? GetCarById(int id);
}

public class CarService : ICarService
{
    private static readonly List<Car> Cars = new()
    {
        new Car
        {
            Id = 1,
            Make = "Toyota",
            Model = "Corolla",
            Year = 2023,
            Category = "Economy",
            PricePerDay = 49.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 2,
            Make = "Honda",
            Model = "Civic",
            Year = 2022,
            Category = "Compact",
            PricePerDay = 54.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 3,
            Make = "Ford",
            Model = "Mustang",
            Year = 2023,
            Category = "Sport",
            PricePerDay = 99.99,
            IsAvailable = false
        },
        new Car
        {
            Id = 4,
            Make = "BMW",
            Model = "X5",
            Year = 2024,
            Category = "SUV",
            PricePerDay = 129.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 5,
            Make = "Tesla",
            Model = "Model 3",
            Year = 2024,
            Category = "Electric",
            PricePerDay = 89.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 6,
            Make = "Chevrolet",
            Model = "Silverado",
            Year = 2023,
            Category = "Truck",
            PricePerDay = 79.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 7,
            Make = "Audi",
            Model = "A4",
            Year = 2023,
            Category = "Sedan",
            PricePerDay = 109.99,
            IsAvailable = false
        },
        new Car
        {
            Id = 8,
            Make = "Mazda",
            Model = "CX-5",
            Year = 2022,
            Category = "SUV",
            PricePerDay = 69.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 9,
            Make = "Hyundai",
            Model = "Elantra",
            Year = 2023,
            Category = "Economy",
            PricePerDay = 44.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 10,
            Make = "Kia",
            Model = "Sportage",
            Year = 2023,
            Category = "SUV",
            PricePerDay = 74.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 11,
            Make = "Volkswagen",
            Model = "Jetta",
            Year = 2022,
            Category = "Compact",
            PricePerDay = 59.99,
            IsAvailable = false
        },
        new Car
        {
            Id = 12,
            Make = "Mercedes",
            Model = "C-Class",
            Year = 2023,
            Category = "Luxury",
            PricePerDay = 149.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 13,
            Make = "Lexus",
            Model = "RX",
            Year = 2024,
            Category = "Luxury SUV",
            PricePerDay = 159.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 14,
            Make = "Nissan",
            Model = "Altima",
            Year = 2023,
            Category = "Sedan",
            PricePerDay = 64.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 15,
            Make = "Dodge",
            Model = "Charger",
            Year = 2023,
            Category = "Sport",
            PricePerDay = 109.99,
            IsAvailable = false
        },
        new Car
        {
            Id = 16,
            Make = "Porsche",
            Model = "911 GT3 RS",
            Year = 2024,
            Category = "Sports",
            PricePerDay = 400.00,
            IsAvailable = true
        },
        new Car
        {
            Id = 17,
            Make = "Jeep",
            Model = "Wrangler",
            Year = 2023,
            Category = "Off-Road",
            PricePerDay = 84.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 18,
            Make = "Subaru",
            Model = "Outback",
            Year = 2022,
            Category = "SUV",
            PricePerDay = 69.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 19,
            Make = "Volvo",
            Model = "XC60",
            Year = 2023,
            Category = "Premium SUV",
            PricePerDay = 119.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 20,
            Make = "Jaguar",
            Model = "F-TYPE",
            Year = 2023,
            Category = "Sports",
            PricePerDay = 189.99,
            IsAvailable = false
        },
        new Car
        {
            Id = 21,
            Make = "Genesis",
            Model = "G70",
            Year = 2023,
            Category = "Luxury",
            PricePerDay = 134.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 22,
            Make = "Infiniti",
            Model = "QX50",
            Year = 2023,
            Category = "Luxury SUV",
            PricePerDay = 144.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 23,
            Make = "Cadillac",
            Model = "Escalade",
            Year = 2024,
            Category = "Full-Size SUV",
            PricePerDay = 179.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 24,
            Make = "Acura",
            Model = "MDX",
            Year = 2023,
            Category = "Premium SUV",
            PricePerDay = 124.99,
            IsAvailable = false
        },
        new Car
        {
            Id = 25,
            Make = "Buick",
            Model = "Enclave",
            Year = 2022,
            Category = "SUV",
            PricePerDay = 89.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 26,
            Make = "Rivian",
            Model = "R1T",
            Year = 2024,
            Category = "Electric Truck",
            PricePerDay = 199.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 27,
            Make = "Lucid",
            Model = "Air",
            Year = 2024,
            Category = "Electric Luxury",
            PricePerDay = 219.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 28,
            Make = "Polestar",
            Model = "2",
            Year = 2023,
            Category = "Electric",
            PricePerDay = 119.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 29,
            Make = "Chevrolet",
            Model = "Blazer",
            Year = 2023,
            Category = "SUV",
            PricePerDay = 79.99,
            IsAvailable = true
        },
        new Car
        {
            Id = 30,
            Make = "GMC",
            Model = "Yukon",
            Year = 2023,
            Category = "Full-Size SUV",
            PricePerDay = 159.99,
            IsAvailable = false
        }
    };

    public List<Car> GetAllCars()
    {
        return Cars;
    }

    public Car? GetCarById(int id)
    {
        return Cars.FirstOrDefault(c => c.Id == id);
    }
}

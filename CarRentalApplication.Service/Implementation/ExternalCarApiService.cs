using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Domain.DTO;
using CarRentalApplication.Repository;
using CarRentalApplication.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarRentalApplication.Service.Implementation
{

    public class ExternalCarApiService : IExternalCarApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IRepository<Car> _carRepository;

        private const string API_BASE_URL = "http://localhost:5000/api/cars";

        public ExternalCarApiService(
            HttpClient httpClient,
            IRepository<Car> carRepository)
        {
            _httpClient = httpClient;
            _carRepository = carRepository;
        }

        public async Task<List<Car>> FetchAndSaveCarsAsync()
        {
            try
            {

                var externalCars = await FetchCarsFromApiAsync();

                if (!externalCars.Any())
                {
                    return new List<Car>();
                }

                var savedCars = await SaveCarsToDatabase(externalCars);

                return savedCars;
            }
            catch (Exception ex)
            {
                return new List<Car>();
            }
        }

        private async Task<List<ExternalCarDTO>> FetchCarsFromApiAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(API_BASE_URL);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<ExternalCarDTO>();
                }

                var jsonContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var cars = JsonSerializer.Deserialize<List<ExternalCarDTO>>(jsonContent, options);

                return cars ?? new List<ExternalCarDTO>();
            }
            catch (HttpRequestException ex)
            {
                return new List<ExternalCarDTO>();
            }
            catch (JsonException ex)
            {
                return new List<ExternalCarDTO>();
            }
        }

        private async Task<List<Car>> SaveCarsToDatabase(List<ExternalCarDTO> externalCars)
        {
            var savedCars = new List<Car>();

            var existingCars = _carRepository.GetAll(selector: x => x).ToList();

            foreach (var externalCar in externalCars)
            {
                try
                {
                    var existingCar = existingCars.FirstOrDefault(c =>
                        c.Make == externalCar.Make &&
                        c.Model == externalCar.Model &&
                        c.Year == externalCar.Year);

                    if (existingCar != null)
                    {
                        savedCars.Add(existingCar);
                        continue;
                    }

                    var car = new Car
                    {
                        Id = Guid.NewGuid(),
                        Make = externalCar.Make,
                        Model = externalCar.Model,
                        Year = externalCar.Year,
                        Category = externalCar.Category,
                        PricePerDay = externalCar.PricePerDay,
                        IsAvailable = externalCar.IsAvailable
                    };

                    var savedCar = _carRepository.Insert(car);
                    savedCars.Add(savedCar);

                }
                catch (Exception ex)
                {
                }
            }

            return savedCars;
        }
    }
}

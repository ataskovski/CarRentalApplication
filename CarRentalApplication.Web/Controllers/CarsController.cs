    using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Domain.DTO;
using CarRentalApplication.Service.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Security.Claims;
    using System.Threading.Tasks;

    namespace CarRentalApplication.Web.Controllers
    {
        public class CarsController : Controller
        {
            private readonly ICarService _carService;
            private readonly IWishlistService _wishlistService;
            private readonly IExternalCarApiService _externalCarApiService;

        public CarsController(
            ICarService carService, 
            IWishlistService wishlistService,
            IExternalCarApiService externalCarApiService)
        {
            _carService = carService;
            _wishlistService = wishlistService;
            _externalCarApiService = externalCarApiService;
        }


        public async Task<IActionResult> Index()
        {

            await _externalCarApiService.FetchAndSaveCarsAsync();

            var cars = _carService.GetAll();
            return View(cars);
        }

        // GET: Cars/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carService.GetById(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Make,Model,Year,Category,PricePerDay,IsAvailable,Id")] Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.Add(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carService.GetById(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Make,Model,Year,Category,PricePerDay,IsAvailable,Id")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _carService.Update(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carService.GetById(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _carService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddToWishlist(Guid id)
        {
            var carDto = _wishlistService.GetCarInfo(id);
            return View(carDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishlist(AddToWishlistDTO model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _carService.AddToWishlist(model, Guid.Parse(userId));
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
            {
                return _carService.GetById(id) != null ? true : false;
            }
        }
    }

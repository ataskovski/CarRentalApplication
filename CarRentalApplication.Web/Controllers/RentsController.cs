using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace CarRentalApplication.Web.Controllers
{
    public class RentsController : Controller
    {
        private readonly IRentService _rentService;

        public RentsController(IRentService rentService)
        {
            _rentService = rentService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var rents = _rentService.GetByUserId(userId).ToList();

            return View(rents);
        }

        public IActionResult Details(Guid id)
        {
            var rent = _rentService.GetById(id);
            if (rent == null)
                return NotFound();

            return View(rent);
        }

        [HttpPost]
        public IActionResult Finish(Guid id)
        {
            var rent = _rentService.GetById(id);
            if (rent == null)
                return NotFound();

            if (rent.IsFinished)
                return BadRequest("Rent already finished");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var ok = _rentService.FinishRent(rent.Id, userId);
            if (!ok)
                return BadRequest();

            return RedirectToAction("Details", new { id = rent.Id });
        }

        public IActionResult Download(Guid id)
        {
            var rent = _rentService.GetById(id);
            if (rent == null)
                return NotFound();

            if (!rent.IsFinished)
                return BadRequest("Rent is not finished yet");

            var bytes = _rentService.GenerateInvoice(rent.Id);
            if (bytes == null || bytes.Length == 0)
                return BadRequest("Unable to generate invoice");

            return File(bytes, "application/pdf", $"invoice_{rent.Id}.pdf");
        }
    }
}

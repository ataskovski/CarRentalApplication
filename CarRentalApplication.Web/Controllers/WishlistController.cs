using CarRentalApplication.Service.Implementation;
using CarRentalApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalApplication.Web.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Challenge();
            var wishlist = _wishlistService.GetByUserIdIncludingCars(Guid.Parse(userId));
            return View(wishlist);
        }

        public IActionResult DeleteFromWishlist(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _wishlistService.DeleteFromWishlist(id, userId);
            return RedirectToAction("Index");
        }
        public IActionResult Rent()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _wishlistService.RentCars(userId);
            return RedirectToAction("Index");
        }
    }
}

using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Service.Interface
{
    public interface IWishlistService
    {
        Wishlist GetByUserId(Guid id);
        WishlistDTO GetByUserIdIncludingCars(Guid id);
        AddToWishlistDTO GetCarInfo(Guid id);
        Boolean DeleteFromWishlist(Guid id, string userId);
        Boolean RentCars(string userId);
        Boolean FinishRent(Guid rentId, string userId);
    }
}

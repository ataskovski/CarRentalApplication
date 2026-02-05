using CarRentalApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;

namespace CarRentalApplication.Service.Interface
{
    public interface IRentService
    {
        IEnumerable<Rent> GetByUserId(string userId);
        Rent? GetById(Guid id);
        bool FinishRent(Guid rentId, string userId);
        byte[] GenerateInvoice(Guid rentId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApplication.Domain.DomainModels;

namespace CarRentalApplication.Service.Interface
{

    public interface IExternalCarApiService
    {
        Task<List<Car>> FetchAndSaveCarsAsync();
    }
}

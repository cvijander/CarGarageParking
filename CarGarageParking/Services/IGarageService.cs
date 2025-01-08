using CarGarageParking.Models;
using System.Diagnostics.Metrics;

namespace CarGarageParking.Services
{
    public interface IGarageService
    {
        IEnumerable<Garage> GetAllGarages();

        Garage GetGarageById(int id);

        void AddGarage(Garage garage);

        void Update(Garage garage);

        void Delete(int id);


    }
}

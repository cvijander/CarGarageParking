using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IVehicleInGarageService
    {
        IEnumerable<VehicleInGarage> GetAllVehicleInGarage();
        VehicleInGarage GetVehicleInGarage(int id);

        void AddVehicleInGarage(VehicleInGarage vehicleInGarage);

        void Update(VehicleInGarage vehicleInGarage);

        void Delete(int id);
        
    }
}

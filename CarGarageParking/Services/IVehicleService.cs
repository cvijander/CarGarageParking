using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> GetAllVehicles();

        IEnumerable<Vehicle> GetVehicleByCondition(Func<Vehicle, bool> predicate);

        Vehicle GetVehicleById(int id);

        void CreateVehicle(Vehicle vehicle);

        void UpdateVehicle(Vehicle vehicle);

        void DeleteVehicle(int id);
    }
}

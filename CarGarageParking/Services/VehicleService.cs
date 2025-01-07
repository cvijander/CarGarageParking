using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CarGarageParkingDBContext _context;

        public VehicleService(CarGarageParkingDBContext context)
        {
            _context = context;
        }


        public void CreateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void DeleteVehicle(int id)
        {
            Vehicle vehicle = _context.Vehicles.Find(id);

            if(vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _context.Vehicles.ToList();
        }

        public IEnumerable<Vehicle> GetVehicleByCondition(Func<Vehicle, bool> predicate)
        {
            return _context.Vehicles.Where(predicate).ToList();
        }

        public Vehicle GetVehicleById(int id)
        {
            return _context.Vehicles.Find(id);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        
    }
}

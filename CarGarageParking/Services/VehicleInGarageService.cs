
using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Services
{
    public class VehicleInGarageService : IVehicleInGarageService
    {
        private readonly CarGarageParkingDBContext _context;

        public VehicleInGarageService(CarGarageParkingDBContext context)
        {
            _context = context;
        }

        public void AddVehicleInGarage(VehicleInGarage vehicleInGarage)
        {
            _context.VehicleInGarages.Add(vehicleInGarage);
        }

       

        public void Delete(int id)
        {
            var vehicle = _context.VehicleInGarages.Find(id);

            if(vehicle != null)
            {
                _context.VehicleInGarages.Remove(vehicle);
            }
        }

        public IEnumerable<VehicleInGarage> GetAllVehicleInGarage()
        {
            return _context.VehicleInGarages.Include(v => v.Vehicle).ToList();
        }

        public VehicleInGarage GetVehicleInGarage(int id)
        {
            return  _context.VehicleInGarages.Find(id);
        }

        public void Update(VehicleInGarage vehicleInGarage)
        {
            _context.VehicleInGarages.Update(vehicleInGarage);
        }
                          
        
    }
}

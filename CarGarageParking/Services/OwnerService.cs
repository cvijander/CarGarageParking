using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly CarGarageParkingDBContext _context;

        public OwnerService(CarGarageParkingDBContext context)
        {
            _context = context;
        }


        public void CreateOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            
        }

        public void DeleteOwner(int id)
        {
           Owner owner = _context.Owners.Find(id);

            if(owner != null)
            {
                _context.Owners.Remove(owner);
                 
            }
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _context.Owners.ToList();
        }

        public IEnumerable<Owner> GetAllOwnersWithVehicles()
        {
            return _context.Owners.Include(o => o.Vehicles).ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _context.Owners.Find(id);
        }

        public void UpdateOwner(Owner owner)
        {
            _context.Owners.Update(owner);
            
        }
    }
}

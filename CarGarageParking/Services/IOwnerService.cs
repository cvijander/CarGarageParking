using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IOwnerService
    {
        IEnumerable<Owner> GetAllOwners();

        IEnumerable<Owner> GetAllOwnersWithVehicles();

        Owner GetOwnerById(int id);

        void CreateOwner(Owner owner);

        void UpdateOwner(Owner owner);

        void DeleteOwner(int id);
    }
}

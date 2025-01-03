using CarGarageParking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace CarGarageParking.Controllers
{
    public class OwnerController : Controller
    {
        public IActionResult Index(string firstName, string lastName, int? numberOfCars)
        {

            IEnumerable<Owner> owners = GetAllOwners();

            if(firstName !=null)
            {
                owners = owners.Where(o => o.FirstName.ToLower() == firstName.Trim().ToLower()).ToList();
            }

            if(lastName !=null)
            {
                owners = owners.Where(o => o.LastName.ToLower() == lastName.Trim().ToLower()).ToList();
            }

            if(numberOfCars.HasValue)
            {
                owners = owners.Where(o => o.Vehicles.Count() == numberOfCars);
            }

            return View(owners);
        }

        public IActionResult Info(int id)
        {
            IEnumerable<Owner> owners = GetAllOwners();

            Owner singleOwner = owners.FirstOrDefault(o => o.OwnerId == id);


            return View(singleOwner);
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            List<Owner> owners = new List<Owner>();

            Owner owner1 = new Owner();
            owner1.OwnerId = 1;
            owner1.FirstName = "Petar";
            owner1.LastName = "Petrovic";
            owner1.Vehicles = new List<Vehicle>();

            Vehicle opel1 = new Vehicle();
            opel1.LicencePlate = "Bg12345";
            owner1.Vehicles.Add(opel1);

            owners.Add(owner1);

            Owner owner2 = new Owner();
            owner2.OwnerId = 2;
            owner2.FirstName = "Jovan";
            owner2.LastName = "Jovanovic";
            owner2.Vehicles = new List<Vehicle>();

            Vehicle vehicle2 = new Vehicle();
            vehicle2.LicencePlate = "SU123456";
            owner2.Vehicles.Add(vehicle2);
            Vehicle vehicel6 = new Vehicle();
            vehicel6.LicencePlate = "VS9856";
            owner2.Vehicles.Add(vehicel6);

            owners.Add(owner2);

            Owner owner3 = new Owner();
            owner3.OwnerId = 3;
            owner3.FirstName = "Milica";
            owner3.LastName = "Milicic";
            owner3.Vehicles = new List<Vehicle>();

            Vehicle vehicle3 = new Vehicle();
            vehicle3.LicencePlate = "NS987654";
            owner3.Vehicles.Add(vehicle3);
            Vehicle vehicle4 = new Vehicle();
            vehicle4.LicencePlate = "NS123";
            owner3.Vehicles.Add(vehicle4);
            Vehicle vehicle5 = new Vehicle();
            vehicle5.LicencePlate = "NS654";
            owner3.Vehicles.Add(vehicle5);

            owners.Add(owner3);

            return owners;
        }
    }
}

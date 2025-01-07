using CarGarageParking.Models;
using CarGarageParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CarGarageParking.Controllers
{
    public class OwnerController : Controller
    {

        private readonly IOwnerService _ownerService;

        private readonly IVehicleService _vehicleService;

      

        public OwnerController(IOwnerService ownerService, IVehicleService vehicleService)
        {
            _ownerService = ownerService;
            _vehicleService = vehicleService;

        }

        public IActionResult Index(string firstName, string lastName, int? numberOfCars)
        {

            var owners = _ownerService.GetAllOwnersWithVehicles();
                 

            if (firstName !=null)
            {
                owners = owners.Where(o => o.FirstName.ToLower() == firstName.Trim().ToLower());
            }

            if(lastName !=null)
            {
                owners = owners.Where(o => o.LastName.ToLower() == lastName.Trim().ToLower());
            }

            if(numberOfCars.HasValue)
            {
                owners = owners.Where(o => o.Vehicles.Count() == numberOfCars);
            }

            return View(owners);
        }

        public IActionResult Info(int id)
        {
           Owner singleOwner = _ownerService.GetOwnerById(id);            
           singleOwner.Vehicles = _vehicleService.GetVehicleByCondition(v => v.OwnerId == id).ToList();      


            return View(singleOwner);
        }

        
    }
}

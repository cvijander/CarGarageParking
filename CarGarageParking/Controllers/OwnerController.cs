using CarGarageParking.Models;
using CarGarageParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CarGarageParking.Controllers
{
    public class OwnerController : Controller
    {
             

        private readonly IUnitOfWork _unitOfWork;
           

        public OwnerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string firstName, string lastName, int? numberOfCars)
        {

            var owners =  _unitOfWork.OwnerService.GetAllOwnersWithVehicles();
                 

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
           Owner singleOwner =  _unitOfWork.OwnerService.GetOwnerById(id);            
           singleOwner.Vehicles = _unitOfWork.VehicleService.GetVehicleByCondition(v => v.OwnerId == id).ToList();      


            return View(singleOwner);
        }

        
    }
}

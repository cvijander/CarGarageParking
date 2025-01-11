using CarGarageParking.Models;
using CarGarageParking.Services;
using CarGarageParking.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace CarGarageParking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult EnterVehicle()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchAGarage(string? search)
        {
            IEnumerable<Garage> garages = _unitOfWork.GarageService.GetAllGarages();

            

            if(!string.IsNullOrEmpty(search))
            {
                var result = search.Trim().ToLower();

                garages = _unitOfWork.GarageService.GetAllGarages().Where(g => g.Name.ToLower().Contains(result) || g.Location.ToLower().Contains(result));
            }

            if (!garages.Any())
            {
                ViewBag.ErrorMessage = "No garages found for your search.";
            }
            
            return View("GarageResult", garages);
        }

       

        [HttpGet]
        public IActionResult EnterVehicleDetails(int id)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(id);

            if(garage == null)
            {
                return NotFound();
            }

            EnterVehicleModel evm = new EnterVehicleModel();

            evm.GarageId = garage.GarageId;
            evm.GarageName = garage.Name;
            evm.GarageLocation = garage.Location;          
            

            return View(evm);
        }

        [HttpPost]
        public IActionResult ConfirmVehicleEntry(int garageId, string licencePlate)
        {
            var existingVehicle = _unitOfWork.VehicleInGarageService.GetAllVehicleInGarage().FirstOrDefault(v => v.Vehicle != null && v.Vehicle.LicencePlate == licencePlate && v.IsVehicleStillInGarage);

            if(existingVehicle != null)
            {
                ViewBag.ErrorMessage = "Vehicle is in garege";
                return View("GarageResult", _unitOfWork.GarageService.GetAllGarages());
            }

            Garage garage = _unitOfWork.GarageService.GetGarageById(garageId);
            if(garage == null || garage.IsFull)
            {
                ViewBag.ErrorMessage = "Garage is full or not found";
                return View("GarageResult", _unitOfWork.GarageService.GetAllGarages());
            }

            VehicleInGarage vig = new VehicleInGarage();
            vig.GarageId = garageId;
            vig.Vehicle = new Vehicle();
            vig.Vehicle.LicencePlate = licencePlate;
            vig.EntryTime = DateTime.Now;
            vig.HourlyRate = 25;

            garage.CurrentOccupancy++;

            _unitOfWork.VehicleInGarageService.AddVehicleInGarage(vig);
            _unitOfWork.SaveChanges();

            ViewBag.SuccessMessage = $"Vehilce with licence plate {licencePlate} has entered the garage {garage.Name} at {vig.EntryTime} ";

            return View("GarageResult", _unitOfWork.GarageService.GetAllGarages());


        }

        [HttpGet]
        public IActionResult ExitVehicle()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

    
        public IActionResult Privacy()
        {
            return View();
        }

        
        
    }
}

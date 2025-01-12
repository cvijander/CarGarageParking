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
        public IActionResult SearchAGarage(string? search, int page = 1)
        {
            IEnumerable<Garage> garages = _unitOfWork.GarageService.GetAllGarages();            

            if(!string.IsNullOrEmpty(search))
            {
                var result = search.Trim().ToLower();

                garages = garages.Where(g => g.Name.ToLower().Contains(result) || g.Location.ToLower().Contains(result));
            }

            if (!garages.Any())
            {
                ViewBag.ErrorMessage = "No garages found for your search.";
            }

            
            var pageSize = 2;

            PaginationViewModel<Garage> pgvm = new PaginationViewModel<Garage>();
            pgvm.TotalCount = garages.Count();
            pgvm.CurrentPage = page;            
            pgvm.PageSize = pageSize;
            garages = garages.Skip(pageSize * (page - 1)).Take(pageSize);
            pgvm.Collection = garages;

            return View("GarageResult", pgvm );
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
                ViewBag.ErrorMessage = "Vehicle is already in garege";

                var garages = _unitOfWork.GarageService.GetAllGarages();
                var pageSize = 2;
                var pgvm = new PaginationViewModel<Garage>
                {
                    TotalCount = garages.Count(),
                    CurrentPage = 1,
                    PageSize = pageSize,
                    Collection = garages.Take(pageSize)
                };

                return View("GarageResult", pgvm);
                
            }

            Garage garage = _unitOfWork.GarageService.GetGarageById(garageId);
            if(garage == null || garage.IsFull)
            {
                ViewBag.ErrorMessage = "Garage is full or not found";
                var garages = _unitOfWork.GarageService.GetAllGarages();
                var pageSize = 2;
                var pgvm = new PaginationViewModel<Garage>
                {
                    TotalCount = garages.Count(),
                    CurrentPage = 1, 
                    PageSize = pageSize,
                    Collection = garages.Take(pageSize)
                };

                return View("GarageResult", pgvm);
                
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

            var garagesAfterEnrty = _unitOfWork.GarageService.GetAllGarages();

            var pgvmFinal = new PaginationViewModel<Garage>
            {
                TotalCount = garagesAfterEnrty.Count(),
                CurrentPage = 1,
                PageSize = 2,
                Collection = garagesAfterEnrty.Take(2).ToList()
            };

            ViewData["CurrentStep"] = 4;
            ViewData["IsConfirmationPage"] = true;

            return View("GarageResult", pgvmFinal);
            

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

using CarGarageParking.Models;
using CarGarageParking.Services;
using CarGarageParking.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Newtonsoft.Json;

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

            if (!string.IsNullOrEmpty(search))
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

            return View(pgvm);
        }



        [HttpGet]
        public IActionResult EnterVehicleDetails(int id)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(id);

            if (garage == null)
            {
                return NotFound();
            }

            EnterVehicleModel evm = new EnterVehicleModel();

            evm.GarageId = garage.GarageId;
            evm.Garage = garage;

            return View(evm);
        }

        [HttpPost]
        public IActionResult ConfirmVehicleEntry(int garageId, string licencePlate)
        {
            var existingVehicle = _unitOfWork.VehicleInGarageService.GetAllVehicleInGarage().FirstOrDefault(v => v.Vehicle != null && v.Vehicle.LicencePlate == licencePlate && v.IsVehicleStillInGarage);

            if (existingVehicle != null)
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

                return View("SearchAGarage", pgvm);

            }

            Garage garage = _unitOfWork.GarageService.GetGarageById(garageId);
            if (garage == null || garage.IsFull)
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

                return View("SearchAGarage", pgvm);

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

            return View("SearchAGarage", pgvmFinal);


        }

        [HttpGet]
        public IActionResult ExitVehicle()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RegisterUser()
        {
            var model = new ApplicationRegistrationViewModel();
            model.Owner = new Owner();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(ApplicationRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            return RedirectToAction("VehicleCount", new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName

            });
        }

        [HttpGet]
        public IActionResult VehicleCount(string firstName, string lastName)
        {
            var model = new ApplicationRegistrationViewModel();
            model.Owner = new Owner();
            model.Owner.FirstName = firstName;
            model.Owner.LastName = lastName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VehicleCount(ApplicationRegistrationViewModel model)
        {
            if (model.NumberOfVehicles < 1 || model.NumberOfVehicles > 10)
            {
                ModelState.AddModelError("", "You can register between 1 to 10");
                return View(model);
            }



            for (int i = 0; i < model.NumberOfVehicles; i++)
            {
                model.Vehicles.Add(new Vehicle());
            }


            return RedirectToAction("LicenceInput", new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName,
                numberOfVehicles = model.NumberOfVehicles
            });
        }


        [HttpGet]
        public IActionResult LicenceInput(string firstName, string lastName, int numberOfVehicles)
        {
            try
            {
                var model = new ApplicationRegistrationViewModel();
                model.Owner = new Owner();
                model.Owner.FirstName = firstName;
                model.Owner.LastName = lastName;
                model.NumberOfVehicles = numberOfVehicles;
                model.Vehicles = new List<Vehicle>();
               

                for (int i = 0; i < numberOfVehicles; i++)
                {
                    model.Vehicles.Add(new Vehicle());
                }

                return View(model);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                throw;
            }
        }
     


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LicenceInput(ApplicationRegistrationViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("LicenceInput", model);
            }                                 

            return RedirectToAction("ConfirmApplication", new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName,
                numberOfVehicles = model.NumberOfVehicles,
                licencePlates = string.Join(",",model.Vehicles.Select(v => v.LicencePlate))
            });
        }


        [HttpGet]
        public IActionResult ConfirmApplication(string firstName, string lastName, int numberOfVehicles, string licencePlates)
        {
            var model = new ApplicationRegistrationViewModel();
            model.Owner =  new Owner();
            model.Owner.FirstName = firstName;
            model.Owner.LastName = lastName;
            model.NumberOfVehicles = numberOfVehicles;
            model.Vehicles = licencePlates.Split(',').Select(lp => new Vehicle { LicencePlate = lp }).ToList();
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmApplication(ApplicationRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ConfirmationApplication", model);
            }

            Application application = new Application();
            application.Owner = model.Owner;
            application.Vehicles = model.Vehicles;
            application.Credit = 100;
            application.HasActiveMembership = true;

            _unitOfWork.ApplicationService.AddApplication(application);
            _unitOfWork.SaveChanges();

            

            
            return RedirectToAction("Success" , new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName,
                numberOfVehicles = model.NumberOfVehicles,
                licencePlates = string.Join(",",model.Vehicles.Select(v => v.LicencePlate))
            });
        }

        [HttpGet]
        public IActionResult Success (string firstName, string lastName, int numberOfVehicles,string licencePlates)
        {
            var model = new ApplicationRegistrationViewModel();
            model.Owner.FirstName = firstName;
            model.Owner.LastName = lastName;
            model.NumberOfVehicles = numberOfVehicles;
            model.Vehicles = licencePlates.Split(',').Select(lp => new Vehicle { LicencePlate = lp }).ToList();

            ViewBag.SuccessMessage = "Application registered successfully!";

            return View(model); 
        }


        public IActionResult Privacy()
        {
            return View();
        }

        
        
    }
}

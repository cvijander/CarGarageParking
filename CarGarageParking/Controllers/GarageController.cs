using CarGarageParking.Models;
using CarGarageParking.Services;
using CarGarageParking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class GarageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GarageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string name, string location, int? maxCapacity, int? availableSpots, decimal? percent, int page = 1)
        {
            
            var garages = _unitOfWork.GarageService.GetAllGarages();


            if(name !=null)
            {
                garages = garages.Where(g => g.Name.ToLower() == name.Trim().ToLower());
            }

            if(location != null)
            {
                garages = garages.Where(g => g.Location.ToLower() == location.Trim().ToLower());
            }

            if (maxCapacity.HasValue)
            {
                garages = garages.Where(g => g.Capacity >= maxCapacity);
            }
            if (availableSpots.HasValue) 
            {
                garages = garages.Where(g => g.AvailableSpots >= availableSpots);   
            }

            var pageSize = 2;

            PaginationViewModel<Garage> pgmvGarage   = new PaginationViewModel<Garage>();
            pgmvGarage.PageSize = pageSize;
            pgmvGarage.TotalCount = garages.Count();

            pgmvGarage.CurrentPage = page;   

            garages = garages.Skip( pageSize * (page -1) ).Take(pageSize);

            pgmvGarage.Collection = garages;

            return View(pgmvGarage);
        }

        public IActionResult Info(int id)
        {
           Garage garage = _unitOfWork.GarageService.GetGarageById(id);

            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Garage garage)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GarageService.AddGarage(garage);
                _unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(garage);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(id);

            if(garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Garage garage)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GarageService.Update(garage);
                _unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(garage);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(id);

            if(garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if(id == 0)
            {
                return BadRequest("Invalid garage id");
            }

            Garage garage = _unitOfWork.GarageService.GetGarageById(id);

             if(garage == null)
            {
                return NotFound();
            }

            _unitOfWork.GarageService.Delete(id);
            _unitOfWork.SaveChanges();

            

            return RedirectToAction(nameof(Index));
        }
       
    }
}

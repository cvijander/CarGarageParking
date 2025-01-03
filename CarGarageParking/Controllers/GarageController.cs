using CarGarageParking.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class GarageController : Controller
    {
        public IActionResult Index(string name, string location, int? maxCapacity, int? availableSpots, decimal? percent)
        {
            IEnumerable<Garage> garages = ShowAllGarages();

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
                        

            
            return View(garages);
        }

        public IActionResult Info(int id)
        {
            IEnumerable<Garage> garages = ShowAllGarages();
            Garage garage = garages.FirstOrDefault(g => g.GarageId == id);

            return View(garage);
        }

        private IEnumerable<Garage> ShowAllGarages()
        {
            List<Garage> listOfGarages = new List<Garage>();
            Garage g1 = new Garage();
            g1.GarageId = 1;
            g1.Name = "Resavska";
            g1.Location = "Beograd";
            g1.Capacity = 600;
            g1.CurrentOccupancy = 450;
            listOfGarages.Add(g1);

            Garage g2 = new Garage();
            g2.GarageId = 2;
            g2.Name = "Centar";
            g2.Location = "Novi Sad";
            g2.Capacity = 400;
            g2.CurrentOccupancy = 250;
            listOfGarages.Add(g2);

            return listOfGarages;

        }
       
    }
}

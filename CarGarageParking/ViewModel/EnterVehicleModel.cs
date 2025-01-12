using CarGarageParking.Models;

namespace CarGarageParking.ViewModel
{
    public class EnterVehicleModel
    {
        public int GarageId { get; set; }        
       
        public Garage Garage { get; set; }        

        public string? LicencePlate { get; set; }

        
    }
}

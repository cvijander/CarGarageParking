using CarGarageParking.Models;
using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.ViewModel
{
    public class ApplicationRegistrationViewModel
    {
        [Required]
        public Owner Owner { get; set; }

        [Range(1, 10, ErrorMessage = "You can register between 1 and 10 vehicles.")]
        public int NumberOfVehicles { get; set; } = 1;

        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public ApplicationRegistrationViewModel()
        {
            Owner = new Owner(); 
            Vehicles = new List<Vehicle>(); 
        }
    }
}

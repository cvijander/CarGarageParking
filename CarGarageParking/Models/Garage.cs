using CarGarageParking.Util;
using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Models
{
    public class Garage
    {
        public int GarageId { get; set; }

        [Required(ErrorMessage ="Garage name is required.")]
        [StringLength(100,ErrorMessage =" Garage name can not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Location is required.")]
        [StringLength(150,ErrorMessage = "Garage location length can not exceed 150 characters.")]
        public string Location { get; set; }

        [Range(1, int.MaxValue,ErrorMessage ="Capacity must be greater than zero")]
        public int Capacity { get; set; }

        [Range(0,int.MaxValue,ErrorMessage = "Current occupancy can not be nagetive.")]
        [IntTypeGreaterThan("Capacity", ErrorMessage = "Capacity  must be greater or equal than current capacity.")]
        public int CurrentOccupancy { get; set; }

        public int AvailableSpots { get
            {
                return Capacity - CurrentOccupancy;
            }
        }

        public ICollection<VehicleInGarage> VehicleInGarage { get;set; } = new List<VehicleInGarage>(); 

        public bool IsFull { get
            {
                 return CurrentOccupancy >= Capacity;
            } 
        }

    }
}

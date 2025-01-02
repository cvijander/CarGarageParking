namespace CarGarageParking.Models
{
    public class Garage
    {
        public int GarageId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }

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

namespace CarGarageParking.Models
{
    public class Garage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }

        public List<VehicleInGarage> VehicleInGarage { get;set; } = new List<VehicleInGarage>(); 
    }
}

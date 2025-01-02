using Microsoft.AspNetCore.Http.HttpResults;

namespace CarGarageParking.Models
{
    public class VehicleInGarage
    {
        public int VehicleInGarageId { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public int GarageId { get; set; }

        public Garage Garage  { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public decimal HourlyRate { get; set; }

        public int? OwnerId { get; set; }

        public Owner? Owner { get; set; }

        public bool IsVehicleStillInGarage { get; set; } = true;    
        

    }
}

using Microsoft.AspNetCore.Http.HttpResults;

namespace CarGarageParking.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string LicencePlate { get; set; }

        public int? OwnerId { get; set; }

        public Owner? Owner { get; set; }
    }
}

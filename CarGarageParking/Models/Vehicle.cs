using Microsoft.AspNetCore.Http.HttpResults;

namespace CarGarageParking.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string LicencePlate { get; set; }

        public string Owner { get; set; }
    }
}

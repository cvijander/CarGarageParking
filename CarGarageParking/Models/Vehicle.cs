using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Licence plate is required.")]
        [StringLength(15,ErrorMessage = "Licence plate can not exced 15 characters.")]

        public string LicencePlate { get; set; }

        public int? OwnerId { get; set; }

        public Owner? Owner { get; set; }
    }
}

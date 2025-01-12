using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.AccessControl;

namespace CarGarageParking.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "First name is reqired.")]
        [StringLength(50,ErrorMessage ="First name can not exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage ="Last name can not exceed 50 characters.")]
        public string LastName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public ICollection<VehicleInGarage> VehicleInGarages { get; set; } = new List<VehicleInGarage>();


    }
}

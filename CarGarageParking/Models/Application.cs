using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        [Range(0, double.MaxValue ,ErrorMessage = "Credit must be graeter than zero.")]
        public decimal Credit { get; set; }

        public bool HasActiveMembership { get; set; }   
    }
}

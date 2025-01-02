namespace CarGarageParking.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public decimal Credit { get; set; }

        public bool HasActiveMembership { get; set; }   
    }
}

namespace CarGarageParking.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}

namespace CarGarageParking.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();

    }
}

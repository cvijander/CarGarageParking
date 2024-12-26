namespace CarGarageParking.Models
{
    public class Payment
    {
        public int Id { get; set; } 

        public decimal Amount { get; set; }

        public DateTime PaymentTime { get; set; }

        public int VehicleInGarageId { get; set; }

        public VehicleInGarage VehicleInGarage { get; set; } = null!;
    }
}

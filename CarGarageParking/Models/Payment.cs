using CarGarageParking.Util;
using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total charge must be greather than zero")]
        [IntTypeGreaterThan("VehicleHorlyRate", ErrorMessage = "Total charge   must be greater or equal than hourly rate.")]
        public decimal TotalCharge { get; set; }

        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Payment time is required.")]
        [DataType(DataType.DateTime)]

        public DateTime PaymentTime { get; set; }

        [DateGreaterThan("PaymentTime", ErrorMessage = "Expiration date must be greater than payment time.")]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationTime { get; set; }  // payment time + 15 minuta ili krece novi obracun 

        [Required]
        public int VehicleInGarageId { get; set; }


        public decimal VehicleHourlyRate { get; set; }
        public VehicleInGarage VehicleInGarage { get; set; } = null!;
    }
}

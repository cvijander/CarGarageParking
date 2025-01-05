using CarGarageParking.Util;
using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Models
{
    public class Payment :IValidatableObject
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

        [Range(0.01,double.MaxValue, ErrorMessage = "Vehicle hourly rate must be greather than zero.")]
        public decimal VehicleHourlyRate { get; set; }
        public VehicleInGarage VehicleInGarage { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsPaid)
            {
                yield return new ValidationResult("Payment has not been completed", new[] { nameof(IsPaid)});
            }

            if ((DateTime.Now - PaymentTime).TotalMinutes > 15)
            {
                VehicleInGarage.EntryTime = ExpirationTime;

                yield return new ValidationResult("You have exceed time to leave a garage, new cycle has started.", new[] { nameof(ExpirationTime) });
            }

            var totalHours = Math.Ceiling((PaymentTime - VehicleInGarage.EntryTime).TotalHours);
            var reiquiredCharge = (decimal)totalHours * VehicleHourlyRate;

            if(TotalCharge < reiquiredCharge)
            {
                yield return new ValidationResult($"Total charge must be at least {reiquiredCharge} based on hourly rate", new[] {nameof(TotalCharge) });
            }
        }
    }
}

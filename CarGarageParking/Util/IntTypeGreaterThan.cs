using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Util
{
    public class IntTypeGreaterThan :ValidationAttribute
    {
           private readonly string _comparisonProperty;

            public IntTypeGreaterThan(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (value == null)
                {
                    return ValidationResult.Success;
                }

                var currentValue = (int)value;

                var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

                if (comparisonProperty == null)
                {
                    return new ValidationResult($"Uknown property {_comparisonProperty}");
                }

                var comparisonValue = (int)comparisonProperty.GetValue(validationContext.ObjectInstance);

                if (value == null || comparisonValue == null)
                {
                    return ValidationResult.Success;
                }

                if (value != null && comparisonValue != null && currentValue > comparisonValue)
                {
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater then {_comparisonProperty}");
                }

                return ValidationResult.Success;
            }


        }
    }




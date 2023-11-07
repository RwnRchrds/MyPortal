using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Attributes
{
    internal class CurrencyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if ((decimal)value >= 0m)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Amount cannot be less than £0.00.");
            }

            return ValidationResult.Success;
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Attributes;

public class PositiveAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is double dbl)
        {
            if (dbl >= 0)
            {
                return ValidationResult.Success;
            }
        }
        else if (value is decimal dec)
        {
            if (dec >= 0)
            {
                return ValidationResult.Success;
            }
        }
        else if (value is int integer)
        {
            if (integer >= 0)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Value cannot be less than 0.");
    }
}
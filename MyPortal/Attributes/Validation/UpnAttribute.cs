using System.ComponentModel.DataAnnotations;
using MyPortal.Extensions;

namespace MyPortal.Attributes.Validation
{
    public class UpnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            return StudentExtensions.ValidateUpn(value.ToString())
                ? ValidationResult.Success
                : new ValidationResult("Upn is not valid");
        }
    }
}
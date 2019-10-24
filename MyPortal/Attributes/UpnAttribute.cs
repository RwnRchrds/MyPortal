using System.ComponentModel.DataAnnotations;
using MyPortal.Processes;

namespace MyPortal.Attributes
{
    public class UpnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            return SystemProcesses.ValidateUpn(value.ToString())
                ? ValidationResult.Success
                : new ValidationResult("Upn is not valid");
        }
    }
}
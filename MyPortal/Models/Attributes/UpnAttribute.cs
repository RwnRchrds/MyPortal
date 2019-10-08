using System.ComponentModel.DataAnnotations;
using MyPortal.Processes;

namespace MyPortal.Models.Attributes
{
    public class UpnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return SystemProcesses.ValidateUpn(value.ToString())
                ? ValidationResult.Success
                : new ValidationResult("Upn is not valid");
        }
    }
}
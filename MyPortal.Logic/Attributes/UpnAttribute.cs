using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Attributes
{
    internal class UpnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValidationHelper.ValidateUpn((string)value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("UPN is invalid.");
        }
    }
}
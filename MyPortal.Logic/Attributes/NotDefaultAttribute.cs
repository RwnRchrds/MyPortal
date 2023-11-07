using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Attributes
{
    internal class NotDefaultAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value == default ? new ValidationResult("Value cannot be default.") : ValidationResult.Success;
        }
    }
}
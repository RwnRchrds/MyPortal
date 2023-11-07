using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Attributes
{
    internal class NhsNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValidationHelper.ValidateNhsNumber((string)value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("NHS number is invalid.");
        }
    }
}
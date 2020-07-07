using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Attributes
{
    public class NhsNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValidationHelper.ValidateNhsNumber((string) value))
            {
                return ValidationResult.Success;
            }

            else
            {
                return new ValidationResult("NHS number is invalid.");
            }
        }
    }
}
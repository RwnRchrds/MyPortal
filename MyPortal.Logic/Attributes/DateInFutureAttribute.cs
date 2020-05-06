using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyPortal.Logic.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime) value;

            if (date > DateTime.Today)
            {
                return ValidationResult.Success;
            }
            
            return new ValidationResult("Date must be in the future.");
        }
    }
}
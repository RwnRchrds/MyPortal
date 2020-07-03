using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyPortal.Logic.Attributes
{
    public class DateInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime?) value;

            if (date != null && date < DateTime.Today)
            {
                return new ValidationResult("Date cannot be in the past.");
            }

            return ValidationResult.Success;
        }
    }
}
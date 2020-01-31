using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Attributes
{
    public class CurrencyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((decimal) value >= 0m)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Amount cannot be less than £0.00.");
        }
    }
}

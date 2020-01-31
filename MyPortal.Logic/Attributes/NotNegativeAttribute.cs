using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Attributes
{
    public class NotNegativeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((decimal) value < 0)
            {
                return new ValidationResult("Value cannot be less than 0.");
            }

            return ValidationResult.Success;
        }
    }
}

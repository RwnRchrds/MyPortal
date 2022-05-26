using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Attributes
{
    internal class NotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Guid guid)
            {
                return guid == Guid.Empty ? new ValidationResult("Value cannot be empty.") : ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}

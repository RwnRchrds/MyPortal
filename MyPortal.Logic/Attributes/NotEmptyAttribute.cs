using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Attributes
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Guid guid)
            {
                if (guid != Guid.Empty)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Value cannot be empty.");
            }

            return ValidationResult.Success;
        }
    }
}

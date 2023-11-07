using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Attributes;

internal class NotNullAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var guidValue = (Guid?)value;

        if (guidValue != null && guidValue != Guid.Empty)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Value cannot be null.");
    }
}
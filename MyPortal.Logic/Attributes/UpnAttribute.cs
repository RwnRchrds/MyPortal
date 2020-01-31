using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Attributes
{
    public class UpnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValidationHelper.ValidateUpn((string) value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("UPN is invalid.");
        }
    }
}

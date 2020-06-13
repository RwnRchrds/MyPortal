using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;

namespace MyPortal.Logic.Attributes
{
    public class ColourCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = value.ToString() ?? string.Empty;

            if (Regex.IsMatch(stringValue, @"^#(?:[0-9a-fA-F]{3}){1,2}$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Value must be a valid colour hex code.");
        }
    }
}

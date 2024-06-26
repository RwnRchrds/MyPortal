﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyPortal.Logic.Constants;

namespace MyPortal.Logic.Attributes
{
    internal class GenderAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                var validValues = new[] { Sexes.Male, Sexes.Female, Sexes.Other, Sexes.Unknown };

                if (validValues.Contains(stringValue))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Gender is invalid.");
        }
    }
}
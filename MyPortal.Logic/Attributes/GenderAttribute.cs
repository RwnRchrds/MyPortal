using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Constants;

namespace MyPortal.Logic.Attributes
{
    internal class GenderAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                var validValues = new string[] { Gender.Male, Gender.Female, Gender.Other, Gender.Unknown };

                if (validValues.Contains(stringValue))
                {
                    return ValidationResult.Success;
                }
            }
            
            return new ValidationResult("Value must be a valid gender option.");
        }
    }
}
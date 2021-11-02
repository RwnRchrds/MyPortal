using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Logic.Attributes
{
    public class GenderAttribute : ValidationAttribute
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
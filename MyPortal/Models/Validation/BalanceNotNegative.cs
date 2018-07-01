using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models.Validation
{
    public class BalanceNotNegative : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var student = (Student) validationContext.ObjectInstance;

            return student.AccountBalance >= 0
                ? ValidationResult.Success
                : new ValidationResult("Account Balance cannot be negative.");
        }
    }
}
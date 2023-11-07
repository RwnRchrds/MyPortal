using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Constants;
using MyPortal.Logic.Models.Data.Assessment;

namespace MyPortal.Logic.Attributes
{
    internal class RequiredIfMarkAspectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var aspect = (AspectModel)validationContext.ObjectInstance;

            if ((aspect.TypeId == AspectTypes.MarkDecimal || aspect.TypeId == AspectTypes.MarkInteger) &&
                aspect.MaxMark == null)
            {
                return new ValidationResult("Max mark is required.");
            }

            if (aspect.TypeId == AspectTypes.MarkInteger && (aspect.MaxMark % 1 != 0))
            {
                return new ValidationResult("Mark must be whole number.");
            }

            return ValidationResult.Success;
        }
    }
}
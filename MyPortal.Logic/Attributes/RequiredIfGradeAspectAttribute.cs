using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Constants;
using MyPortal.Logic.Models.Data.Assessment;

namespace MyPortal.Logic.Attributes
{
    internal class RequiredIfGradeAspectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var aspect = (AspectModel)validationContext.ObjectInstance;

            if (aspect.TypeId == AspectTypes.Grade && aspect.GradeSetId == null)
            {
                return new ValidationResult("Grade set is required.");
            }

            return ValidationResult.Success;
        }
    }
}
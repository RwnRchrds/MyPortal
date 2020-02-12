using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models;
using MyPortal.Logic.Dictionaries;

namespace MyPortal.Logic.Attributes
{
    public class RequiredIfGradeAspectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var aspect = (Aspect) validationContext.ObjectInstance;

            if (aspect.TypeId == AspectTypeDictionary.Grade && aspect.GradeSetId == null)
            {
                return new ValidationResult("Grade set is required.");
            }

            return ValidationResult.Success;
        }
    }
}

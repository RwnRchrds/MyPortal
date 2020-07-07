using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Attributes
{
    public class RequiredIfGradeAspectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var aspect = (AspectModel) validationContext.ObjectInstance;

            if (aspect.TypeId == AspectTypes.Grade && aspect.GradeSetId == null)
            {
                return new ValidationResult("Grade set is required.");
            }

            return ValidationResult.Success;
        }
    }
}

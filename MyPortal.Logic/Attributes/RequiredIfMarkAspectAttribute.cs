using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models;
using MyPortal.Logic.Constants;

namespace MyPortal.Logic.Attributes
{
    public class RequiredIfMarkAspectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var aspect = (Aspect) validationContext.ObjectInstance;

            if ((aspect.TypeId == AspectTypes.MarkDecimal || aspect.TypeId == AspectTypes.MarkInteger) && aspect.MaxMark == null)
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

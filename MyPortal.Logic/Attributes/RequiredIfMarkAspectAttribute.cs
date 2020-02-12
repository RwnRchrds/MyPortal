using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models;
using MyPortal.Logic.Dictionaries;

namespace MyPortal.Logic.Attributes
{
    public class RequiredIfMarkAspectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var aspect = (Aspect) validationContext.ObjectInstance;

            if ((aspect.TypeId == AspectTypeDictionary.MarkDecimal || aspect.TypeId == AspectTypeDictionary.MarkInteger) && aspect.MaxMark == null)
            {
                return new ValidationResult("Max mark is required.");
            }

            if (aspect.TypeId == AspectTypeDictionary.MarkInteger && (aspect.MaxMark % 1 != 0))
            {
                return new ValidationResult("Mark must be whole number.");
            }

            return ValidationResult.Success;
        }
    }
}
